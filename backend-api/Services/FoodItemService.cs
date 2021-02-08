using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Controllers.Resources.FoodItemResources;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Domain.Services;
using backend_api.Domain.Services.Communication;
using backend_api.Services.Helpers;

namespace backend_api.Services
{
    /// <summary>
    /// The food item service.
    /// Handles the creating, updating and deleting food items
    /// </summary>
    public class FoodItemService : IFoodItemService
    {
        private readonly IFoodItemRepository _foodItemRepository;
        private readonly IUserService _userService;
        private readonly IDiaryEntryService _diaryEntryService;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Handles dependencies
        /// </summary>
        /// <param name="foodItemRepository"></param>
        /// <param name="userService"></param>
        /// <param name="diaryEntryService"></param>
        /// <param name="unitOfWork"></param>
        public FoodItemService(IFoodItemRepository foodItemRepository, IUserService userService, IDiaryEntryService diaryEntryService, IUnitOfWork unitOfWork)
        {
            _foodItemRepository = foodItemRepository;
            _userService = userService;
            _diaryEntryService = diaryEntryService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds a new food item to a users diary entry for a given date.
        /// If the user has no diary entry for that date, then one will be created.
        /// Updates the total calories for the diary entry.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>
        /// Unsuccessful FoodItemResponse if the user does not exist;
        /// Unsuccessful FoodItemResponse if the string date is not in the format dd-mm-yyyy;
        /// Unsuccessful FoodItemResponse if the date is in the future;
        /// Unsuccessful FoodItemResponse if there was an issue adding a new diary entry;
        /// Unsuccessful FoodItemResponse if there was an issue adding the food item to the diary entry;
        /// Successful FoodItemResponse with the food item
        /// </returns>
        public async Task<FoodItemResponse> AddFoodItemAsync(AddFoodItemResource resource)
        {
            // validate userId
            var userResult = await _userService.GetUserAync(resource.UserId);
            if (!userResult.Success)
                return new FoodItemResponse(userResult.Message);

            // validate and convert stringDate to DateTime
            DateTime date;
            try
            {
                date = StringDateHelper.ConverStringDateToDate(resource.StringDate);
            }
            catch (ArgumentException e)
            {
                return new FoodItemResponse(e.Message);
            }

            if (date.Date >= DateTime.Now.AddDays(1))
                return new FoodItemResponse("Can't add a food item in the future");

            DiaryEntry diaryEntry;

            // check if user has a diary entry for the day
            var existingDiaryResult = await _diaryEntryService.GetUsersDiaryEntryForDateAsync(resource.UserId, date);

            if (!existingDiaryResult.Success) 
            {   // no diary entry for that day, create one
                var diaryEntryResult = await _diaryEntryService.AddDiaryEntryAsync(resource.UserId, date);
                
                if (!diaryEntryResult.Success)
                    return new FoodItemResponse($"Failed to add a new food item: {diaryEntryResult.Message}");

                diaryEntry = diaryEntryResult.DiaryEntry;
            }
            else 
            {   // user has a diary entry for that day
                diaryEntry = existingDiaryResult.DiaryEntry;
            }
            
            // create food item
            FoodItem foodItem = new FoodItem
            {
                Name = resource.Name,
                Calories = resource.Calories,
                DiaryEntry = diaryEntry,
                User = userResult.User
            };

            // update the total calories for the diary entry
            diaryEntry.TotalCalories += resource.Calories;

            // save the new food item and the changes to the diary entry
            try
            {
                await _foodItemRepository.AddAsync(foodItem);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return new FoodItemResponse($"An error occurred when saving the food item: {e}");
            }
            
            return new FoodItemResponse(foodItem);
        }

        /// <summary>
        /// Updates an existing food item in the database.
        /// Updates the total calories for the diary entry the food item belongs to.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="foodItem"></param>
        /// <returns>
        /// Unsuccessful FoodItemResponse if food item does not exist;
        /// Unsuccessful FoodItemResponse if there was an issue updating the food item in the database;
        /// Successful FoodItemResponse with the food item
        /// </returns>
        public async Task<FoodItemResponse> UpdateFoodItemAsync(int id, FoodItem foodItem)
        {
            // check if the food item exists
            var existingFoodItem = await _foodItemRepository.GetAsync(id);
            if (existingFoodItem == null)
                return new FoodItemResponse($"Food item {id} not found");

            // remove the old calories from the total calories
            existingFoodItem.DiaryEntry.TotalCalories -= existingFoodItem.Calories;
            // add the new calories to the total calories
            existingFoodItem.DiaryEntry.TotalCalories += foodItem.Calories;

            // update the name and calories of the old food item
            existingFoodItem.Name = foodItem.Name;
            existingFoodItem.Calories = foodItem.Calories;

            // save the changes
            try
            {
                _foodItemRepository.Update(existingFoodItem);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return new FoodItemResponse($"An error occurred when updating food item {id}: {e}");
            }

            return new FoodItemResponse(existingFoodItem);
        }

        /// <summary>
        /// Delete a food item from the database.
        /// Updates the total calories for the diary entry the food item belongs to.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Unsuccessful FoodItemResponse if the food item does not exist;
        /// Unsuccessful FoodItemResponse if there was an issue removing the food item from the database;
        /// Successful FoodItemResponse with the food item
        /// </returns>
        public async Task<FoodItemResponse> DeleteFoodItemAsync(int id)
        {
            // check if the food item exists
            var existingFoodItem = await _foodItemRepository.GetAsync(id);
            if (existingFoodItem == null)
                return new FoodItemResponse($"Food item {id} not found");

            var diaryEntry = existingFoodItem.DiaryEntry;

            // remove the calories from the total calories
            diaryEntry.TotalCalories -= existingFoodItem.Calories;

            // check if this food item is the last one in the diary entry
            var foodItems = existingFoodItem.DiaryEntry.FoodItems;
            bool deleteDiaryEntry = false;
            if (foodItems.Count == 1 && foodItems[0].Id == existingFoodItem.Id)
                deleteDiaryEntry = true; // mark the diary entry for deletion

            // remove the food item from the database and save the changes to the diary entry
            try
            {
                _foodItemRepository.Remove(existingFoodItem);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return new FoodItemResponse($"An error occurred when deleting diary entry {id}: {e}");
            }

            // wait until after the food item is deleted, as the food item is a child of the diary
            if (deleteDiaryEntry)
            {
                var deleteResult = await _diaryEntryService.DeleteDiaryEntryAsync(diaryEntry.Id);
                if (!deleteResult.Success)
                    return new FoodItemResponse($"Failed to delete diary entry: {deleteResult}");
            }

            return new FoodItemResponse(existingFoodItem);
        }
    }
}
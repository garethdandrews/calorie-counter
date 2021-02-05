using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Domain.Services;
using backend_api.Domain.Services.Communication;
using backend_api.Resources.DiaryEntryResources;
using backend_api.Resources.FoodItemResources;
using backend_api.Services.Helpers;

namespace backend_api.Services
{
    public class FoodItemService : IFoodItemService
    {
        private readonly IFoodItemRepository _foodItemRepository;
        private readonly IUserService _userService;
        private readonly IDiaryEntryService _diaryEntryService;
        private readonly IUnitOfWork _unitOfWork;

        public FoodItemService(IFoodItemRepository foodItemRepository, IUserService userService, IDiaryEntryService diaryEntryService, IUnitOfWork unitOfWork)
        {
            _foodItemRepository = foodItemRepository;
            _userService = userService;
            _diaryEntryService = diaryEntryService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FoodItem>> ListAsync()
        {
            return await _foodItemRepository.ListAsync();
        }

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

            // CHECK IF THIS IS ACTUALLY UPDATING IN THE DATABASE
            // if not, we need to call the update method, but also need an id for the existing diaryEntry, so need to get that before
            diaryEntry.TotalCalories += resource.Calories;

            // var updateDiaryEntryResult = await _diaryEntryService.UpdateDiaryEntryAsync(id, diaryEntry);
            // if (!updateDiaryEntryResult.Success)
            //     return new FoodItemResponse($"An error has occurred when saving the food item: {updateDiaryEntryResult.Message}");

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

        public async Task<FoodItemResponse> UpdateFoodItemAsync(int id, FoodItem foodItem)
        {
            var existingFoodItem = await _foodItemRepository.GetAsync(id);

            if (existingFoodItem == null)
                return new FoodItemResponse($"Food item {id} not found");

            existingFoodItem.DiaryEntry.TotalCalories -= existingFoodItem.Calories;
            existingFoodItem.DiaryEntry.TotalCalories += foodItem.Calories;

            existingFoodItem.Name = foodItem.Name;
            existingFoodItem.Calories = foodItem.Calories;

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

        public async Task<FoodItemResponse> DeleteFoodItemAsync(int id)
        {
            var existingFoodItem = await _foodItemRepository.GetAsync(id);

            if (existingFoodItem == null)
                return new FoodItemResponse($"Food item {id} not found");

            // CHECK IF THIS WORKS - may need to get the diary entry and then update it
            existingFoodItem.DiaryEntry.TotalCalories -= existingFoodItem.Calories;

            try
            {
                _foodItemRepository.Remove(existingFoodItem);
                await _unitOfWork.CompleteAsync();

                return new FoodItemResponse(existingFoodItem);
            }
            catch (Exception e)
            {
                return new FoodItemResponse($"An error occurred when deleting diary entry {id}: {e}");
            }
        }
    }
}
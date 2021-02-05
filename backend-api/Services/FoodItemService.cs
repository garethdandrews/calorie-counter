using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Domain.Services;
using backend_api.Domain.Services.Communication;
using backend_api.Resources.DiaryEntryResources;
using backend_api.Services.Helpers;

namespace backend_api.Services
{
    public class FoodItemService : IFoodItemService
    {
        private readonly IFoodItemRepository _foodItemRepository;
        private readonly IDiaryEntryService _diaryEntryService;

        public FoodItemService(IFoodItemRepository foodItemRepository, IDiaryEntryService diaryEntryService)
        {
            _foodItemRepository = foodItemRepository;
            _diaryEntryService = diaryEntryService;
        }

        public async Task<IEnumerable<FoodItem>> ListAsync()
        {
            return await _foodItemRepository.ListAsync();
        }

        public async Task<FoodItemResponse> AddFoodItemAsync(int userId, string stringDate, string name, int calories)
        {
            // Check if diary entry exists for the date
            // If not create one
            // Get Id of diary entry
            // Create a food item object
            DateTime date;
            try
            {
                date = StringDateHelper.ConverStringDateToDate(stringDate)
            }
            catch (ArgumentException e)
            {
                return new FoodItemResponse(e.Message);
            }

            var result = await _diaryEntryService.GetDiaryEntry(userId, date);
            
            // get user object
            User user = null;
            // get calorie goal from user

            DiaryEntry diaryEntry = result.DiaryEntry;

            if (diaryEntry == null)
            {
                diaryEntry = new DiaryEntry
                {
                    Date = date,
                    TotalCalories = 2500,
                    User = user
                };
                // ADD A METHOD TO ADD A DIARY ENTRY

            }
            
            FoodItem foodItem = new FoodItem
            {
                Name = name,
                Calories = calories,
                DiaryEntry = diaryEntry,
                User = user
            };

            diaryEntry.FoodItems.Add(foodItem);

            await _foodItemRepository.AddFoodItemAsync(foodItem);
            return new FoodItemResponse(foodItem);

        }

    }
}
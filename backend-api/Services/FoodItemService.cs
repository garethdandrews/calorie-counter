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

        public FoodItemService(IFoodItemRepository foodItemRepository, IUserService userService, IDiaryEntryService diaryEntryService)
        {
            _foodItemRepository = foodItemRepository;
            _userService = userService;
            _diaryEntryService = diaryEntryService;
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

            // check if user has a diary entry for the day
            var diaryEntryResult = await _diaryEntryService.GetDiaryEntryAsync(resource.UserId, resource.StringDate);

            if (!diaryEntryResult.Success)
                return new FoodItemResponse(diaryEntryResult.Message);


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
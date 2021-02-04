using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Domain.Services;
using backend_api.Domain.Services.Communication;
using backend_api.Resources.DiaryEntryResources;

namespace backend_api.Services
{
    public class FoodItemService : IFoodItemService
    {
        private IFoodItemRepository _foodItemRepository;

        public FoodItemService(IFoodItemRepository foodItemRepository)
        {
            _foodItemRepository = foodItemRepository;
        }

        public async Task<IEnumerable<FoodItem>> ListAsync()
        {
            return await _foodItemRepository.ListAsync();
        }

        public async Task<AddFoodItemResponse> AddFoodItemAsync(int userId, string stringDate, string name, int calories)
        {
            
        }

    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Controllers.Resources.FoodItemResources;
using backend_api.Domain.Models;
using backend_api.Domain.Services.Communication;

namespace backend_api.Domain.Services
{
    public interface IFoodItemService
    {
        Task<IEnumerable<FoodItem>> ListAsync();
        Task<FoodItemResponse> AddFoodItemAsync(AddFoodItemResource resource);
        Task<FoodItemResponse> UpdateFoodItemAsync(int id, FoodItem foodItem);
        Task<FoodItemResponse> DeleteFoodItemAsync(int id);
    }
}
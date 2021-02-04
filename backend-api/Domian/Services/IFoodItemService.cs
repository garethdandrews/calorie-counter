using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Services.Communication;

namespace backend_api.Domain.Services
{
    public interface IFoodItemService
    {
        Task<IEnumerable<FoodItem>> ListAsync();
        Task<FoodItemResponse> AddFoodItemAsync(int userId, string stringDate, string name, int calories);
    }
}
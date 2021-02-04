using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;

namespace backend_api.Domain.Repositories
{
    public interface IFoodItemRepository
    {
        Task<IEnumerable<FoodItem>> ListAsync();
        Task<FoodItem> GetFoodItemAsync(int id);
        Task AddFoodItemAsync(FoodItem foodItem);
        void Update(FoodItem foodItem);
        void Remove(FoodItem foodItem);
    }
}
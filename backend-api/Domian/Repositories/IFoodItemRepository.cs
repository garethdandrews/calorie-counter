using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;

namespace backend_api.Domain.Repositories
{
    public interface IFoodItemRepository
    {
        Task<IEnumerable<FoodItem>> ListAsync();
        Task<FoodItem> GetAsync(int id);
        Task AddAsync(FoodItem foodItem);
        void Update(FoodItem foodItem);
        void Remove(FoodItem foodItem);
    }
}
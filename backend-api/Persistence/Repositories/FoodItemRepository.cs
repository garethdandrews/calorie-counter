using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Persistence.Repositories
{
    public class FoodItemRepository : BaseRepository, IFoodItemRepository
    {
        public FoodItemRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<FoodItem>> ListAsync()
        {
            return await _context.FoodItems.ToListAsync();
        }

        public async Task<FoodItem> GetFoodItemAsync(int id)
        {
            return await _context.FoodItems.FindAsync(id);
        }

        public async Task AddFoodItemAsync(FoodItem foodItem)
        {
            await _context.FoodItems.AddAsync(foodItem);
        }

        public void Update(FoodItem foodItem)
        {
            _context.FoodItems.Update(foodItem);
        }

        public void Remove(FoodItem foodItem)
        {
            _context.FoodItems.Remove(foodItem);
        }
    }
}
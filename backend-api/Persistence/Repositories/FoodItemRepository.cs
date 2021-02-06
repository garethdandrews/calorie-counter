using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Persistence.Repositories
{
    /// <summary>
    /// The food item repository.
    /// Handles CRUD operations
    /// </summary>
    public class FoodItemRepository : BaseRepository, IFoodItemRepository
    {
        /// <summary>
        /// Handles dependencies
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public FoodItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary></summary>
        /// <returns>
        /// A list of food items
        /// </returns>
        public async Task<IEnumerable<FoodItem>> ListAsync()
        {
            return await _context.FoodItems
                    .ToListAsync();
        }

        /// <summary></summary>
        /// <param name="id"></param>
        /// <returns>
        /// A food item for a given ID, returns null if no food item is found
        /// </returns>
        public async Task<FoodItem> GetAsync(int id)
        {
            return await _context.FoodItems
                    .Include(x => x.DiaryEntry)
                    .ThenInclude(x => x.FoodItems)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Add a food item to the database
        /// </summary>
        /// <param name="foodItem"></param>
        /// <returns></returns>
        public async Task AddAsync(FoodItem foodItem)
        {
            await _context.FoodItems
                    .AddAsync(foodItem);
        }

        /// <summary>
        /// Updates a food item in the database
        /// </summary>
        /// <param name="foodItem"></param>
        public void Update(FoodItem foodItem)
        {
            _context.FoodItems
                    .Update(foodItem);
        }

        /// <summary>
        /// Remove a food item from the database
        /// </summary>
        /// <param name="foodItem"></param>
        public void Remove(FoodItem foodItem)
        {
            _context.FoodItems
                    .Remove(foodItem);
        }
    }
}
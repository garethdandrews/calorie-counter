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
    }
}
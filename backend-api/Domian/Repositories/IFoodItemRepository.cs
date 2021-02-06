using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;

namespace backend_api.Domain.Repositories
{
    /// <summary>
    /// The food item repository.
    /// Handles CRUD operations
    /// </summary>
    public interface IFoodItemRepository
    {

        /// <summary></summary>
        /// <returns>
        /// A list of food items
        /// </returns>
        Task<IEnumerable<FoodItem>> ListAsync();

        /// <summary></summary>
        /// <param name="id"></param>
        /// <returns>
        /// A food item for a given ID, returns null if no food item is found
        /// </returns>
        Task<FoodItem> GetAsync(int id);

        /// <summary>
        /// Add a food item to the database
        /// </summary>
        /// <param name="foodItem"></param>
        /// <returns></returns>
        Task AddAsync(FoodItem foodItem);

        /// <summary>
        /// Updates a food item in the database
        /// </summary>
        /// <param name="foodItem"></param>
        void Update(FoodItem foodItem);

        /// <summary>
        /// Remove a food item from the database
        /// </summary>
        /// <param name="foodItem"></param>
        void Remove(FoodItem foodItem);
    }
}
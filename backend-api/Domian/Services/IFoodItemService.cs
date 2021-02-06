using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Controllers.Resources.FoodItemResources;
using backend_api.Domain.Models;
using backend_api.Domain.Services.Communication;

namespace backend_api.Domain.Services
{
    /// <summary>
    /// The food item service.
    /// Handles the creating, updating and deleting food items
    /// </summary>
    public interface IFoodItemService
    {
        /// <summary>
        /// Adds a new food item to a users diary entry for a given date.
        /// If the user has no diary entry for that date, then one will be created.
        /// Updates the total calories for the diary entry.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns>
        /// Unsuccessful FoodItemResponse if the user does not exist;
        /// Unsuccessful FoodItemResponse if the string date is not in the format dd-mm-yyyy;
        /// Unsuccessful FoodItemResponse if the date is in the future;
        /// Unsuccessful FoodItemResponse if there was an issue adding a new diary entry;
        /// Unsuccessful FoodItemResponse if there was an issue adding the food item to the diary entry;
        /// Successful FoodItemResponse with the food item
        /// </returns>
        Task<FoodItemResponse> AddFoodItemAsync(AddFoodItemResource resource);

        /// <summary>
        /// Updates an existing food item in the database.
        /// Updates the total calories for the diary entry the food item belongs to.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="foodItem"></param>
        /// <returns>
        /// Unsuccessful FoodItemResponse if food item does not exist;
        /// Unsuccessful FoodItemResponse if there was an issue updating the food item in the database;
        /// Successful FoodItemResponse 
        /// </returns>
        Task<FoodItemResponse> UpdateFoodItemAsync(int id, FoodItem foodItem);

        /// <summary>
        /// Delete a food item from the database.
        /// Updates the total calories for the diary entry the food item belongs to.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Unsuccessful FoodItemResponse if the food item does not exist;
        /// Unsuccessful FoodItemResponse if there was an issue removing the food item from the database;
        /// Successful FoodItemResponse with the food item
        /// </returns>
        Task<FoodItemResponse> DeleteFoodItemAsync(int id);
    }
}
using System.Threading.Tasks;
using AutoMapper;
using backend_api.Controllers.Resources.FoodItemResources;
using backend_api.Domain.Models;
using backend_api.Domain.Services;
using backend_api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
    /// <summary>
    /// The food item controller.
    /// Handles all incoming requests to add and update a food item
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FoodItemController : Controller
    {
        private readonly IFoodItemService _foodItemService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Handles dependencies
        /// </summary>
        /// <param name="foodItemService"></param>
        /// <param name="mapper"></param>
        public FoodItemController(IFoodItemService foodItemService, IMapper mapper)
        {
            _foodItemService = foodItemService;
            _mapper = mapper;
        }

        /// <summary>
        /// Add a food item
        /// </summary>
        /// <param name="resource">userid (int), stringDate (date in format dd-mm-yyy), name (string), calories (int)</param>
        /// <returns>
        /// bad request if parameters are invalid;
        /// bad request if there was an issue adding the food item;
        /// success response with the food item
        /// </returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync([FromBody] AddFoodItemResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _foodItemService.AddFoodItemAsync(resource);

            if (!result.Success)
                return BadRequest(result.Message);

            var foodItemResource = _mapper.Map<FoodItem, FoodItemResource>(result.FoodItem);
            return Ok(foodItemResource);
        }

        /// <summary>
        /// Update a food item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resource">name (string), calories (int)</param>
        /// <returns>
        /// bad request if parameters are invalid;
        /// bad request if there was an issue updating the food item;
        /// success response with the food item
        /// </returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateFoodItemResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var foodItem = _mapper.Map<UpdateFoodItemResource, FoodItem>(resource);
            var result = await _foodItemService.UpdateFoodItemAsync(id, foodItem);

            if (!result.Success)
                return BadRequest(result.Message);

            var foodItemResource = _mapper.Map<FoodItem, FoodItemResource>(result.FoodItem);
            return Ok(foodItemResource);
        }

        /// <summary>
        /// Deletes a food item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// bad request if there was an issue deleting the food item;
        /// success response with the food item
        /// </returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _foodItemService.DeleteFoodItemAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var foodItemResource = _mapper.Map<FoodItem, FoodItemResource>(result.FoodItem);
            return Ok(foodItemResource);
        }
    }
}
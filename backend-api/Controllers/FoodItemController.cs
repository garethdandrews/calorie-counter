using System.Threading.Tasks;
using AutoMapper;
using backend_api.Controllers.Resources.FoodItemResources;
using backend_api.Domain.Models;
using backend_api.Domain.Services;
using backend_api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
    [Route("api/[controller]")]
    public class FoodItemController : Controller
    {
        private readonly IFoodItemService _foodItemService;
        private readonly IMapper _mapper;

        public FoodItemController(IFoodItemService foodItemService, IMapper mapper)
        {
            _foodItemService = foodItemService;
            _mapper = mapper;
        }

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
using System.Threading.Tasks;
using AutoMapper;
using backend_api.Domain.Models;
using backend_api.Domain.Services;
using backend_api.Extensions;
using backend_api.Resources.FoodItemResources;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
    [Route("api/[controller")]
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
        public async Task<IActionResult> PostAsync(SaveFoodItemResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _foodItemService.AddFoodItemAsync(resource.UserId, resource.StringDate, resource.Name, resource.Calories);

            if (!result.Success)
                return BadRequest(result.Message);

            var foodItemResource = _mapper.Map<FoodItem, FoodItemResource>(result.FoodItem);
            return Ok(foodItemResource);
        }
    }
}
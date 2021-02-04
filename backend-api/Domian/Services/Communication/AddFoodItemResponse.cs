using backend_api.Domain.Models;

namespace backend_api.Domain.Services.Communication
{
    public class AddFoodItemResponse : BaseResponse
    {
        public FoodItem FoodItem { get; private set; }

        private AddFoodItemResponse(bool success, string message, FoodItem foodItem) : base(success, message)
        {
            FoodItem = foodItem;
        }

        // Creates a success response
        public AddFoodItemResponse(FoodItem foodItem) : this(true, string.Empty, foodItem)
        {
        }

        // Creates an error response
        public AddFoodItemResponse(string message) : this(false, message, null)
        {
        }
    }
}
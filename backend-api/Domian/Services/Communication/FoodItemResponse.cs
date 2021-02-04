using backend_api.Domain.Models;

namespace backend_api.Domain.Services.Communication
{
    public class FoodItemResponse : BaseResponse
    {
        public FoodItem FoodItem { get; private set; }

        private FoodItemResponse(bool success, string message, FoodItem foodItem) : base(success, message)
        {
            FoodItem = foodItem;
        }

        // Creates a success response
        public FoodItemResponse(FoodItem foodItem) : this(true, string.Empty, foodItem)
        {
        }

        // Creates an error response
        public FoodItemResponse(string message) : this(false, message, null)
        {
        }
    }
}
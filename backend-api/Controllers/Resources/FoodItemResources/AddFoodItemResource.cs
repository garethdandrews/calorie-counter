using System.ComponentModel.DataAnnotations;

namespace backend_api.Controllers.Resources.FoodItemResources
{
    public class AddFoodItemResource
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string StringDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Calories { get; set; }
    }
}
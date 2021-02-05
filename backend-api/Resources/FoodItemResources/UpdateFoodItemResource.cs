using System.ComponentModel.DataAnnotations;

namespace backend_api.Resources.FoodItemResources
{
    public class UpdateFoodItemResource
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Calories { get; set; }
    }
}
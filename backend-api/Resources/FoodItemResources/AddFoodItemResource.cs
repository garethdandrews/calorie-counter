using System.ComponentModel.DataAnnotations;

namespace backend_api.Resources.FoodItemResources
{
    public class SaveFoodItemResource
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(8)]
        public string StringDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Calories { get; set; }
    }
}
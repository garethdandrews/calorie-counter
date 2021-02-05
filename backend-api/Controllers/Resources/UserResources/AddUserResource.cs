using System.ComponentModel.DataAnnotations;

namespace backend_api.Controllers.UserResources
{
    public class AddUserResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public int CalorieTarget { get; set; }
    }
}
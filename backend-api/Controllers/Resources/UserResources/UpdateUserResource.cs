using System.ComponentModel.DataAnnotations;

namespace backend_api.Controllers.UserResources
{
    public class UpdateUserResource
    {
        [Required]
        public int CalorieTarget { get; set; }
    }
}
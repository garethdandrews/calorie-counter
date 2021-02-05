using System.ComponentModel.DataAnnotations;

namespace backend_api.Resources.UserResources
{
    public class UpdateUserResource
    {
        [Required]
        public int CalorieTarget { get; set; }
    }
}
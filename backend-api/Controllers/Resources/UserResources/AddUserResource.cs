using System.ComponentModel.DataAnnotations;

namespace backend_api.Controllers.Resources.UserResources
{
    public class AddUserResource
    {
        [Required]        
        [StringLength(20, ErrorMessage = "Must be between 6 and 30 characters", MinimumLength = 5)]        
        public string Name { get; set; }

        [Required]        
        [StringLength(20, ErrorMessage = "Must be between 6 and 30 characters", MinimumLength = 5)]   
        public string Password { get; set; }

        [Required]
        public int CalorieTarget { get; set; }
    }
}
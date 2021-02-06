using System.ComponentModel.DataAnnotations;

namespace backend_api.Controllers.Resources.UserResources
{
    public class UserCredentialsResource
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace backend_api.Controllers.Resources.TokenResources
{
    public class RefreshTokenResource
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
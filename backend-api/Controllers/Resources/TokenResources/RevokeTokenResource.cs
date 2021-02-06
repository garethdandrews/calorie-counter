using System.ComponentModel.DataAnnotations;

namespace backend_api.Controllers.Resources.TokenResources
{
    public class RevokeTokenResource
    {
        [Required]
        public string Token { get; set; }
    }
}
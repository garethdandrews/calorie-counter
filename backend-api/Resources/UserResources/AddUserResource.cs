using System.ComponentModel.DataAnnotations;
using backend_api.Domain.Models;

namespace backend_api.Resources.UserResources
{
    public class AddUserResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(EGender))]
        public EGender Gender { get; set; }

        [Required]
        public int CalorieTarget { get; set; }
    }
}
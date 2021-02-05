using System.ComponentModel.DataAnnotations;
using backend_api.Domain.Models;

namespace backend_api.Resources.UserResources
{
    public class UserResource
    {
        public string Name { get; set; }
        public int CalorieTarget { get; set; }
    }
}
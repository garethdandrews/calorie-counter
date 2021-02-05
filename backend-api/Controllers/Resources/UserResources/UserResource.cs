using System.Collections.Generic;

namespace backend_api.Controllers.UserResources
{
    public class UserResource
    {
        public string Name { get; set; }
        public int CalorieTarget { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
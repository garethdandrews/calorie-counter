using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace backend_api.Domain.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new Collection<UserRole>();

        public int CalorieTarget { get; set; }
        public List<DiaryEntry> Diary { get; set; }

    }
}
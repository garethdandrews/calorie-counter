using System.Collections.Generic;

namespace backend_api.Domain.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CalorieTarget { get; set; }
        public List<DiaryEntry> Diary { get; set; }
    }
}
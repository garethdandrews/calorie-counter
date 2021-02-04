using System.Collections.Generic;

namespace backend_api.Domain.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EGender Gender { get; set; }
        public int CalorieTarget { get; set; }
        public IList<DiaryEntry> Diary { get; set; } = new List<DiaryEntry>();
    }
}
using System.Collections.Generic;

namespace backend_api.Domain.Models
{
    public class Diary
    {
        public int Id { get; set; }
        public IList<DiaryEntry> Entries { get; set; }

        public User User { get; set; }
    }
}
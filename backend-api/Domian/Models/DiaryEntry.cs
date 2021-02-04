using System;
using System.Collections.Generic;

namespace backend_api.Domain.Models
{
    public class DiaryEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TotalCalories { get; set; }
        public IList<FoodItem> FoodItems { get; set; } = new List<FoodItem>();


        public User User { get; set; }
    }
}
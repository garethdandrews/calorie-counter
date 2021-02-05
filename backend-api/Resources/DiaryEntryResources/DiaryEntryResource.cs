using System;
using System.Collections.Generic;
using backend_api.Resources.FoodItemResources;

namespace backend_api.Resources.DiaryEntryResources
{
    public class DiaryEntryResource
    {
        public DateTime Date { get; set; }
        public int TotalCalories { get; set; }
        public IList<FoodItemResource> FoodItems { get; set; }
    }
}
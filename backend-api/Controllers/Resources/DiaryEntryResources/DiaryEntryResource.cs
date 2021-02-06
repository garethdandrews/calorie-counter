using System;
using System.Collections.Generic;
using backend_api.Controllers.Resources.FoodItemResources;

namespace backend_api.Controllers.Resources.DiaryEntryResources
{
    public class DiaryEntryResource
    {
        public DateTime Date { get; set; }
        public int TotalCalories { get; set; }
        public IList<FoodItemResource> FoodItems { get; set; }
    }
}
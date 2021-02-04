namespace backend_api.Domain.Models
{
    public class FoodItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }

        public DiaryEntry DiaryEntry { get; set; }
        public Diary Diary { get; set; }
        public User User { get; set; }
    }
}
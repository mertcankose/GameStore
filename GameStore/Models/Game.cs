namespace GameStore.Models
{
    public class Game
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; } // New Rating property
    }
}

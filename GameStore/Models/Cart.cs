namespace GameStore.Models
{
    public class Cart
    {
        public List<long> ProductIds { get; set; }
        public List<Product> Products { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace GameStore.Models
{
    public class ProductContext : DbContext 
    {
        public ProductContext(DbContextOptions<ProductContext> opts) : base(opts) { }
        public DbSet<Product> Products => Set<Product>();
    }
}
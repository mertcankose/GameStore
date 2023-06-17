using Microsoft.EntityFrameworkCore;

namespace GameStore.Models
{
    public class CartContext : DbContext 
    {
        public CartContext(DbContextOptions<CartContext> opts) : base(opts) { }
        public DbSet<Cart> CartProducts => Set<Cart>();
    }
}

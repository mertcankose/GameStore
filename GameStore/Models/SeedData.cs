using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Models
{
    public class SeedData
    {
        public static void SeedDatabaseUser(UserContext context)
        {
            context.Database.Migrate();

            // Existing User seeding code
            if (context.Users.Count() == 0)
            {
                context.Users.AddRange(
                    new User
                    {
                        Username = "admin",
                        Password = "1230,aaa",
                        Email = "admin@admin.com",
                        FirstName = "Admin",
                        LastName = "Admin"
                    }
                );

                context.SaveChanges();
            }

        }

        public static void SeedDatabaseProduct(ProductContext productContext)
        {
           

            if (productContext.Products.Count() == 0)
            {
                productContext.Products.AddRange(
                    new Product
                    {
                        Name = "Legend of Legends",
                        Description = "Good Game",
                        Image = "",
                        Price = 500,
                    },
                                        new Product
                                        {
                                            Name = "Gta V",
                                            Description = "Good Game2 ",
                                            Image = "",
                                            Price = 1000,
                                        }
                  );
            }
            productContext.SaveChanges();
        }

        public static void SeedDatabaseCart(CartContext cartContext)
        {
            cartContext.Database.Migrate();

            if (cartContext.CartProducts.Count() == 0)
            {
                cartContext.CartProducts.AddRange(
                    new Cart
                    {
                        Name = "Legend of Legends",
                        Description = "Good Game",
                        Image = "",
                        Price = 500,
                    }
                  );
            }
            cartContext.SaveChanges();

        }
    }
}
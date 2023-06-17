using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Models
{
    public class SeedData
    {
        public static void SeedDatabaseUser(UserContext userContext)
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

            // New Game seeding code
            if (context.Games.Count() == 0)
            {
                context.Games.AddRange(
                    new Game
                    {
                        Title = "Super Mario Odyssey",
                        Description = "Join Mario on a massive, globe-trotting 3D adventure.",
                        ImageUrl = "https://via.placeholder.com/150",
                        Price = 59.99m,
                        Rating = 4 // Set the rating for the game
                    },
                    new Game
                    {
                        Title = "The Legend of Zelda: Breath of the Wild",
                        Description = "Step into a world of discovery, exploration and adventure.",
                        ImageUrl = "https://via.placeholder.com/150",
                        Price = 59.99m,
                        Rating = 5 // Set the rating for the game
                    }
                // Add more games with ratings as needed
                );

                context.SaveChanges();
            }

        }

        public static void SeedDatabaseProduct(ProductContext productContext)
        {
            productContext.Database.Migrate();

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

    }
}
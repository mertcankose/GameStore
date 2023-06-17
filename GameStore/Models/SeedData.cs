using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Models
{
    public class SeedData
    {
        public static void SeedDatabase(UserContext context)
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

        public static IEnumerable<Game> GetGames(UserContext context)
        {
            return context.Games.ToList();
        }
    }
}
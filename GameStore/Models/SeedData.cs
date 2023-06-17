using Microsoft.EntityFrameworkCore;

namespace GameStore.Models
{
    public class SeedData
    {
        public static void SeedDatabaseUser(UserContext userContext)
        {
            userContext.Database.Migrate();

            if (userContext.Users.Count() == 0)
            {
                userContext.Users.AddRange(
                    new User
                    {
                        Username = "admin",
                        Password = "1230,aaa",
                        Email = "admin@admin.com",
                        FirstName = "Admin",
                        LastName = "Admin"
                    }
                  );
            }
            userContext.SaveChanges();
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

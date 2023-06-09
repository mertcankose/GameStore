using Microsoft.EntityFrameworkCore;

namespace GameStore.Models
{
    public class SeedData
    {


        public static void SeedDatabase(UserContext userContext)
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





    }
}

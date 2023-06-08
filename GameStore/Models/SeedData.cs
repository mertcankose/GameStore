using Microsoft.EntityFrameworkCore;

namespace GameStore.Models
{
    public class SeedData
    {


        public static void SeedDatabase(UserContext context)
        {
            context.Database.Migrate();

            if (context.Users.Count() == 0)
            {
                User user = new User
                {
                    Username = "admin",
                    Password = "1230,aaa",
                    Email = "admin@admin.com",
                    FirstName = "Admin",
                    LastName = "Admin"
                };
            }
            context.SaveChanges();
        }





    }
}

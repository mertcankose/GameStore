using GameStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
  
    public class UsersController : ControllerBase
    {
        private UserContext userContext;

        public UsersController(UserContext userContext)
        {
            this.userContext = userContext;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return userContext.Users;
        }

        [HttpGet("{id}")]
        public User? GetUser(long id, [FromServices] ILogger<UsersController> logger)
        {
            logger.LogDebug("GetUser action invoked");
            return userContext.Users.Find(id);
        }

        [HttpPost]
        public User CreateUser([FromBody] User user)
        {
            userContext.Users.Add(user);
            userContext.SaveChanges();
            return user;
        }

        [HttpPut]
        public User UpdateUser([FromBody] User user)
        {
            userContext.Users.Update(user);
            userContext.SaveChanges();
            return user;
        }

        [HttpDelete("{id}")]
        public void DeleteUser(long id)
        {
            userContext.Users.Remove(new User { Id = id });
            userContext.SaveChanges();
        }
    }
}

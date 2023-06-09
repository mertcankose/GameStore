using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
    public class AccountController : Controller
    {
        public UserContext userContext;

        public AccountController(UserContext userContext)
        {
            this.userContext = userContext;
        }

        /*
        public async Task<IActionResult> Index(long id = 1)
        {
            return View(await userContext.Users.FindAsync(id));
        }
        */

        // return all users
        public async Task<IActionResult> Index()
        {
            return View(await userContext.Users.ToListAsync());
        }

 
    }
}
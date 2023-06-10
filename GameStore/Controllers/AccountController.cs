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

 
        public async Task<IActionResult> Index(long? id)
        {
            if(id !=null)
            {
                return View(await userContext.Users.FindAsync(id));
            } else
            {
                var currentUserEmail = HttpContext.Session.GetString("CurrentUserEmail");
                if (currentUserEmail != null)
                {
                    var user = await userContext.Users.FirstOrDefaultAsync(u => u.Email == currentUserEmail);
                    return View(user);
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }
          
        }

        // Register View
        public IActionResult Register()
        {
            return View();
        }

        // Register Post
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var userExist = await userContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (userExist == null)
                {
                    userContext.Users.Add(user);
                    await userContext.SaveChangesAsync();
                    // redirect login
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    return RedirectToAction(nameof(Register));
                }
            }
            return View(user);
        }

        // Login View
        public IActionResult Login()
        {
            return View();
        }

        // Login Post
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            // check db if exist user redirect homepage if not redirect register
            var userExist = await userContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
            if (userExist != null)
            {
                // Store the logged-in user in session
                HttpContext.Session.SetString("CurrentUserEmail", userExist.Email);

                // Redirect to Account/Index/id
                return RedirectToAction("Index", "Account");
            }
            else
            {
                return RedirectToAction(nameof(Register));
            }
        }

        // Exit
        public IActionResult Exit()
        {
            HttpContext.Session.Remove("CurrentUserEmail");
            return RedirectToAction(nameof(Login));
        }
    }
}
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserContext _userContext;
        private readonly ProductContext _context;

        public HomeController(ILogger<HomeController> logger, UserContext userContext, ProductContext context)
        {
            _logger = logger;
            _userContext = userContext;
            _context = context;
        }

        public IActionResult Index()
        {

            var products = _context.Products.ToList();
            return View(products);

            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
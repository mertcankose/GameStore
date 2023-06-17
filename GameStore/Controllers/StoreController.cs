using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
    public class StoreController : Controller
    {
        public ProductContext productContext;

        public StoreController(ProductContext productContext)
        {
            this.productContext = productContext;
        }

        public async Task<IActionResult> Index(long? id)
        {
            if (id != null)
            {
                return View(await productContext.Products.FindAsync(id));
            }
            else
            {
                return RedirectToAction(nameof(Home));
                
            }
        }

        // AddProduct Post
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var productExist = await productContext.Products.FirstOrDefaultAsync(u => u.Name == product.Name);
                if (productExist == null)
                {
                    productContext.Products.Add(product);
                    await productContext.SaveChangesAsync();
                    // redirect login
                    return RedirectToAction(nameof(Store));
                }
                else
                {
                    return RedirectToAction(nameof(AddProduct));
                }
            }
            return View(product);
        }


        public IActionResult Store()
        {
            return View();
        }

        public IActionResult Home()
        {
            var products = productContext.Products.ToList();
            return View(products);
        }


    }
}
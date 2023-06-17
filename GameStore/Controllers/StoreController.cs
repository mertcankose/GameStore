using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
    public class StoreController : Controller
    {
        public ProductContext productContext;
        public Cart cartContext;

        public StoreController(ProductContext productContext)
        {
            this.productContext = productContext;
            this.cartContext = cartContext;
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
            // only admin can add product
            var currentUserEmail = HttpContext.Session.GetString("CurrentUserEmail");

            if (currentUserEmail != "admin@admin.com")
            {
                return RedirectToAction(nameof(Home));
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var productExist = await productContext.Products.FirstOrDefaultAsync(u => u.Name == product.Name);
                    if (productExist == null)
                    {
                        productContext.Products.Add(product);
                        await productContext.SaveChangesAsync();
                        // redirect login
                        return RedirectToAction(nameof(Home));
                    }
                    else
                    {
                        return RedirectToAction(nameof(AddProduct));
                    }
                }
            }
                return View(product);
        }

        public async Task<List<Product>> GetCartProducts(Cart cart)
        {
            List<Product> cartProducts = new List<Product>();

            foreach (long productId in cart.ProductIds)
            {
                Product product = await productContext.Products.FindAsync(productId);

                if (product != null)
                {
                    cartProducts.Add(product);
                }
            }

            return cartProducts;
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

        public IActionResult AddProduct()
        {
            return View();
        }
        public IActionResult Cart()
        {
            Cart cart = new Cart();
            // Cart and product creation operations go here
            // For example:
            cart.ProductIds = new List<long>() { 1, 2, 3 };
            cart.Products = GetCartProducts(cart).Result;

            return View(cart);
        }
    }
}
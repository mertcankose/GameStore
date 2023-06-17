using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Controllers
{
    public class StoreController : Controller
    {
        public ProductContext productContext;
        public CartContext cartContext;

        public StoreController(ProductContext productContext, CartContext cartContext)
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

        [HttpPost]
        public async Task<IActionResult> Index(Cart cart)
        {

            
            //cartContext.CartProducts.Add(cart);
            //await cartContext.SaveChangesAsync();

            //return View(cart);
            
            

            
            var cartProductExist = await cartContext.CartProducts.FirstOrDefaultAsync(u => u.Name == cart.Name);
                if (cartProductExist == null)
                {
                    cartContext.CartProducts.Add(cart);
                    await cartContext.SaveChangesAsync();

        
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            

            return View(cart);

        }

        [HttpPost]
        public async Task<IActionResult> Home(long id)
        {
            var cart = await cartContext.CartProducts.FindAsync(id);

            if (cart != null)
            {
                cartContext.CartProducts.Remove(cart);
                await cartContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
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
    }
}
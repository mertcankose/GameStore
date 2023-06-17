using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
    public class CartController : Controller
    {
        public CartContext cartContext;

        public CartController(CartContext cartContext)
        {
            this.cartContext = cartContext;
        }

        
        public async Task<IActionResult> Index(long? id)
        {
            if (id != null)
            {
                return View(await cartContext.CartProducts.FindAsync(id));
            }
            else
            {
                return RedirectToAction(nameof(Home));

            }
        }


        // AddProduct Post
        /*
        [HttpPost]
        public async Task<IActionResult> AddProductToCart(Cart cart)
        {
            // only admin can add product

                if (ModelState.IsValid)
                {
                    var cartProductExist = await cartContext.CartProducts.FirstOrDefaultAsync(u => u.Name == cart.Name);
                    if (cartProductExist == null)
                    {
                        cartContext.CartProducts.Add(cart);
                        await cartContext.SaveChangesAsync();
                        // redirect login
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            
            return View(cart);
        }

        */
        //delete spesific product from cart

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


        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult Home()
        {
            var cartProducts = cartContext.CartProducts.ToList();
            return View(cartProducts);
        }

   



    }
}
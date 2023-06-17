using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]

    public class CartItemController : Controller
    {
        private CartContext cartContext;

        public CartItemController(CartContext cartContext)
        {
            this.cartContext = cartContext;
        }

        [HttpGet]
        public IEnumerable<Cart> GetCartItems()
        {
            return cartContext.CartProducts;
        }

        [HttpGet("{id}")]
        public Cart? GetProduct(long id, [FromServices] ILogger<CartItemController> logger)
        {
            logger.LogDebug("GetProduct action invoked");
            return cartContext.CartProducts.Find(id);
        }

        [HttpPost]
        public Cart CreateProduct([FromBody] Cart product)
        {
            cartContext.CartProducts.Add(product);
            cartContext.SaveChanges();
            return product;
        }

        [HttpPut]
        public Cart UpdateProduct([FromBody] Cart product)
        {
            cartContext.CartProducts.Update(product);
            cartContext.SaveChanges();
            return product;
        }

        [HttpDelete("{id}")]
        public void DeleteUser(long id)
        {
            cartContext.CartProducts.Remove(new Cart { Id = id });
            cartContext.SaveChanges();
        }
    }
}

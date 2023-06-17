using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]

    public class ProductsController : ControllerBase
    {
        private ProductContext productContext;

        public ProductsController(ProductContext productContext)
        {
            this.productContext = productContext;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return productContext.Products;
        }

        [HttpGet("{id}")]
        public Product? GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        {
            logger.LogDebug("GetProduct action invoked");
            return productContext.Products.Find(id);
        }

        [HttpPost]
        public Product CreateProduct([FromBody] Product product)
        {
            productContext.Products.Add(product);
            productContext.SaveChanges();
            return product;
        }

        [HttpPut]
        public Product UpdateProduct([FromBody] Product product)
        {
            productContext.Products.Update(product);
            productContext.SaveChanges();
            return product;
        }

        [HttpDelete("{id}")]
        public void DeleteUser(long id)
        {
            productContext.Products.Remove(new Product { Id = id });
            productContext.SaveChanges();
        }
    }
}

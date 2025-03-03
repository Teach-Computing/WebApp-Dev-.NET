using _02_MyWebAppEFCore.Data;
using _02_MyWebAppEFCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _02_MyWebAppEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class B_ProductsCreateController : ControllerBase
    {
        private readonly ProductContext _context;

        public B_ProductsCreateController(ProductContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
            //return CreatedAtAction(nameof(A_ProductsReadController.GetProductById), new { id = product.Id }, product); // Point to GetProductById action in ProductsReadController
        }
    }
}

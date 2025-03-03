using _02_MyWebAppEFCore.Data;
using _02_MyWebAppEFCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _02_MyWebAppEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class A_ProductsReadController : ControllerBase
    {
        private readonly ProductContext _context;

        public A_ProductsReadController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}

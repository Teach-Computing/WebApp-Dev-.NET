using _01_MyWebApiInMemory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _01_MyWebApiInMemory.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class C_HttpVerbsController : ControllerBase
    {
        private static readonly List<Product> _products = new List<Product>()
        {
            new Product { Id = 1, Name = "Laptop", Description = "High-performance laptop", Price = 1200.00m },
            new Product { Id = 2, Name = "Mouse", Description = "Ergonomic wireless mouse", Price = 25.00m },
            new Product { Id = 3, Name = "Keyboard", Description = "Mechanical keyboard", Price = 75.00m }
        };
        private static int _nextProductId = 4; // To simulate auto-incrementing ID

        [HttpGet("products")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_products);
        }

        [HttpGet("products/{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("products")]
        public ActionResult<Product> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid) // Check for model validation errors (if any annotations were added)
            {
                return BadRequest(ModelState); // Return 400 with validation errors
            }

            product.Id = _nextProductId++; // Assign a new ID
            _products.Add(product);

            // Return 201 Created with the created product and location header
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("products/{id}")]
        public IActionResult PutProduct(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound(); // Return 404 if product not found for update
            }

            // Update the existing product's properties
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            return NoContent(); // Return 204 No Content for successful update
        }

        [HttpDelete("products/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var productToRemove = _products.FirstOrDefault(p => p.Id == id);
            if (productToRemove == null)
            {
                return NotFound(); // Return 404 if product not found for deletion
            }

            _products.Remove(productToRemove);

            return NoContent(); // Return 204 No Content for successful deletion
        }
    }
}

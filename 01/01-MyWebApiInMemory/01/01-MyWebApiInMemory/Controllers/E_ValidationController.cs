using _01_MyWebApiInMemory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace _01_MyWebApiInMemory.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class E_ValidationController : ControllerBase
    {
        private static readonly List<ValidatedProduct> _products = new List<ValidatedProduct>()
        {
            new ValidatedProduct { Id = 1, Name = "Laptop", Description = "High-performance laptop", Price = 1200.00m },
            new ValidatedProduct { Id = 2, Name = "Mouse", Description = "Ergonomic wireless mouse", Price = 25.00m },
            new ValidatedProduct { Id = 3, Name = "Keyboard", Description = "Mechanical keyboard", Price = 75.00m }
        };
        private static int _nextProductId = 4;

        [HttpPost("products")]
        public ActionResult<ValidatedProduct> PostProduct([FromBody] ValidatedProduct product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Automatic validation by [ApiController]
            }

            product.Id = _nextProductId++;
            _products.Add(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("products/{id}")]
        public IActionResult PutProduct(int id, [FromBody] ValidatedProduct product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            return NoContent();
        }

        [HttpGet("products/{id}")]
        public ActionResult<ValidatedProduct> GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}

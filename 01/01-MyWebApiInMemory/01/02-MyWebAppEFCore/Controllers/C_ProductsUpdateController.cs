using _02_MyWebAppEFCore.Data;
using _02_MyWebAppEFCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _02_MyWebAppEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C_ProductsUpdateController : ControllerBase
    {
        private readonly ProductContext _context;

        public C_ProductsUpdateController(ProductContext context)
        {
            _context = context;
        }

        [HttpPut("{id}")] // Route is /api/productsupdate/{id} for PUT requests
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("ID mismatch"); // Return 400 if ID in URL doesn't match ID in body
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 if model validation fails (Data Annotations)
            }

            var existingProduct = await _context.Products.FindAsync(id); // Find existing product by ID
            if (existingProduct == null)
            {
                return NotFound(); // Return 404 Not Found if product with given ID doesn't exist
            }

            // Update properties of the existing product entity
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            try
            {
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id)) // Check if product still exists in case of concurrency issue
                {
                    return NotFound();
                }
                else
                {
                    throw; // Re-throw the exception if it's a different concurrency problem
                }
            }

            return NoContent(); // Return 204 No Content for successful update
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}

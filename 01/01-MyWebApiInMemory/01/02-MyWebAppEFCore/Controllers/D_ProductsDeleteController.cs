using _02_MyWebAppEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _02_MyWebAppEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class D_ProductsDeleteController : ControllerBase
    {
        private readonly ProductContext _context;

        public D_ProductsDeleteController(ProductContext context)
        {
            _context = context;
        }

        [HttpDelete("{id}")] // Route is /api/productsdelete/{id} for DELETE requests
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id); // Find the product to delete by ID
            if (product == null)
            {
                return NotFound(); // Return 404 Not Found if product with given ID doesn't exist
            }

            _context.Products.Remove(product); // Mark the product entity for deletion in the DbContext
            await _context.SaveChangesAsync(); // Save changes to the database to perform the deletion

            return NoContent(); // Return 204 No Content for successful deletion
        }
    }
}

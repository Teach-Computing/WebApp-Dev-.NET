using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _01_MyWebApiInMemory.Controllers;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; } // Nullable string
    public decimal Price { get; set; }
}


[Route("api/[controller]")]
[ApiController]
public class B_DataController : ControllerBase
{
    private static readonly List<Product> _products = new List<Product>()
        {
            new Product { Id = 1, Name = "Laptop", Description = "High-performance laptop", Price = 1200.00m },
            new Product { Id = 2, Name = "Mouse", Description = "Ergonomic wireless mouse", Price = 25.00m },
            new Product { Id = 3, Name = "Keyboard", Description = "Mechanical keyboard", Price = 75.00m }
        };

    [HttpGet("products")]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return Ok(_products); // Returns 200 OK with the list of products
    }

    [HttpGet("products/{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound(); // Returns 404 Not Found if product not found
        }
        return Ok(product); // Returns 200 OK with the product
    }
}

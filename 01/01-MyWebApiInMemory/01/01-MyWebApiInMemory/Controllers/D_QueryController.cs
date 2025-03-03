using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _01_MyWebApiInMemory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class D_QueryController : ControllerBase
    {
        // Added Category property to Product class for this controller
        public class Product : Models.Product
        {
            public string? Category { get; set; }
        }


        private static readonly List<Product> _products = new List<Product>()
        {
            new Product { Id = 1, Name = "Laptop", Description = "High-performance laptop", Price = 1200.00m },
            new Product { Id = 2, Name = "Mouse", Description = "Ergonomic wireless mouse", Price = 25.00m, Category = "Accessories" },
            new Product { Id = 3, Name = "Keyboard", Description = "Mechanical keyboard", Price = 75.00m, Category = "Accessories" },
            new Product { Id = 4, Name = "Monitor", Description = "27-inch 4K Monitor", Price = 300.00m, Category = "Accessories" },
            new Product { Id = 5, Name = "Gaming Laptop", Description = "High-end gaming laptop", Price = 1800.00m, Category = "Gaming" }
        };

        
        [HttpGet("products/category")]
        public ActionResult<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return BadRequest("Category query parameter is required.");
            }

            var filteredProducts = _products.Where(p => p.Category != null && p.Category.ToLower() == category.ToLower()).ToList();
            return Ok(filteredProducts);
        }

        [HttpGet("products/search")]
        public ActionResult<IEnumerable<Product>> SearchProducts(string? searchTerm, decimal? minPrice, decimal? maxPrice)
        {
            var filteredProducts = _products.AsQueryable(); // Start with all products as IQueryable for filtering

            if (!string.IsNullOrEmpty(searchTerm))
            {
                filteredProducts = filteredProducts.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                                               p.Description != null && p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            if (minPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Price <= maxPrice.Value);
            }

            return Ok(filteredProducts.ToList());
        }
    }
}

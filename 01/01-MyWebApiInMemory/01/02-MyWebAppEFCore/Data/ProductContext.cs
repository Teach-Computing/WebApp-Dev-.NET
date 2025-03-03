using _02_MyWebAppEFCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace _02_MyWebAppEFCore.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } // DbSet for the Product entity, representing the Products table
    }
}

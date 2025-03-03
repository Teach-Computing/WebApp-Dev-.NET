using System.ComponentModel.DataAnnotations;

namespace _02_MyWebAppEFCore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10000.00.")]
        public decimal Price { get; set; }
    }
}

namespace _01_MyWebApiInMemory.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } // Nullable string
        public decimal Price { get; set; }
    }
}

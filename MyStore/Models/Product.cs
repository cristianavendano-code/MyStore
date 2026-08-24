namespace MyStore.Models
{
    public class Product
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public int Stock { get; set; }
        public required string Description { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}

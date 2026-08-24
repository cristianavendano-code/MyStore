namespace MyStore.DTO.Product
{
    public class ProductCreateDto
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public required string Description { get; set; }
        public required int CategoryId { get; set; }
    }
}

namespace MyStore.DTO.SaleItem
{
    public class SaleItemResponseDto
    {
        public int Id { get; set; }
        public required int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string ProductPrice { get; set; }
        public required int Quantity { get; set; }
        public required decimal Total { get; set; }
    }
}

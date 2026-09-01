namespace MyStore.DTO.SaleItem
{
    public class SaleItemCreateDto
    {
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
        public required int SaleId { get; set; }
    }
}

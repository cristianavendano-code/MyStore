namespace MyStore.DTO.Sale
{
    public class SaleResponseDto
    {
        public int Id { get; set; }
        public required int ClientId { get; set; }
        public required string ClientName { get; set; }
        public required string Status { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
    }
}

namespace MyStore.Models
{
    public class SaleItem
    {
        public int Id { get; set; }
        public required Product Product { get; set; }
        public required decimal Total { get; set; }
        public required int Quantity { get; set; }
        public required Sale Sale { get; set; }
    }
}

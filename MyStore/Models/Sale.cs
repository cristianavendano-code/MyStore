namespace MyStore.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public required Client Client { get; set; }
        public required string Status { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
    }
}

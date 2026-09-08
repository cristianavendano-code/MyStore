namespace MyStore.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public required int ClientId { get; set; }
        public Client? Client { get; set; }
        public string Status { get; set; } = "Pendiente";
        public decimal Total { get; set; } = 0m;
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}

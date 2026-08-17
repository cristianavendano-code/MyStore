namespace MyStore.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public required decimal DiscountPercentage { get; set; }
    }
}

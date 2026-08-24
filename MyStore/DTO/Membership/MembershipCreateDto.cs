namespace MyStore.DTO.Membership
{
    public class MembershipCreateDto
    {
        public required string Description { get; set; }
        public required decimal DiscountPercentage { get; set; }
    }
}

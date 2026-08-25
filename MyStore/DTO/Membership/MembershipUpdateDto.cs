namespace MyStore.DTO.Membership
{
    public class MembershipUpdateDto
    {
        public required string Description { get; set; }
        public required decimal DiscountPercentage { get; set; }
    }
}

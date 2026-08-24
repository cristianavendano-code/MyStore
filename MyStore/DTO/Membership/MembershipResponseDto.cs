namespace MyStore.DTO.Membership
{
    public class MembershipResponseDto
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public required decimal DiscountPercentage { get; set; }
    }
}

namespace MyStore.DTO.Client
{
    public class ClientResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public required string MembershipDescription { get; set; }
    }
}

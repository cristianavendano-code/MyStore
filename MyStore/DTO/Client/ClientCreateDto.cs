namespace MyStore.DTO.Client
{
    public class ClientCreateDto
    {
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
        public int? MembershipId { get; set; }
    }
}

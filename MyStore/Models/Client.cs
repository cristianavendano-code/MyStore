namespace MyStore.Models
{
    public class Client
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Phone {  get; set; }
        public required string Email { get; set; }
        public required string password { get; set; }
        public int MembershipId { get; set; }
        public Membership? Membership { get; set; }
    }
}

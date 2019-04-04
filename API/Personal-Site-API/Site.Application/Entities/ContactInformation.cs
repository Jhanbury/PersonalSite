namespace Site.Application.Entities
{
    public class ContactInformation
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Address Address { get; set; }
    }
}
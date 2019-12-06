using System.Collections.Generic;

namespace Site.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        
        public ICollection<User> Users { get; set; }
    }
}

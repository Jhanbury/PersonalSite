using System.Collections.Generic;

namespace Site.Application.Entities
{
    public class Hobby
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HobbyTypeId { get; set; }
        public HobbyType HobbyType { get; set; }
        public ICollection<UserHobby> UserHobbies { get; set; }
    }
}
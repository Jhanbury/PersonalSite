﻿namespace Site.Application.Entities
{
    public class UserHobby
    {
        public int UserId { get; set; }
        public int HobbyId { get; set; }
        public User User { get; set; }
        public Hobby Hobby { get; set; }
    }
}
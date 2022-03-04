using System;

namespace Api.Models.DTO.User
{
    public class UserGet
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
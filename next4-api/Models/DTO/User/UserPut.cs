using System.ComponentModel.DataAnnotations;

namespace next4_api.Models.DTO.User
{
    public class UserPut
    {

        [Required]
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
     
    }
}
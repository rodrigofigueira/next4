using System.ComponentModel.DataAnnotations;

namespace next4_api.Models.DTO.User
{
    public class UserPost
    {

        [Required]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
     
        [Required]
        public string Password { get; set; }
    }
}
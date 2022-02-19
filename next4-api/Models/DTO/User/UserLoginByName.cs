using System.ComponentModel.DataAnnotations;

namespace next4_api.Models.DTO.User
{
    public class UserLoginByName
    {

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
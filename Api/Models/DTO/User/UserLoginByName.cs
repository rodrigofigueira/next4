using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.User
{
    public class UserLoginByName
    {

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace next4_api.Models.DTO.User
{
    public class UserPutPassword
    {
        
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string OldPassword { get; set; }
     
        [Required]
        public string NewPassword { get; set; }
    }
}


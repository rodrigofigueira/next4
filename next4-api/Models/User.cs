using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace next4_api.Models
{
    [Table("snna_users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [StringLength(100)]
        [Column("name_view")]
        public string Name { get; set; }
        
        [StringLength(100)]
        [Column("email")]
        public string Email { get; set; }
        
        [StringLength(200)]
        [Column("password")]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        [Column("dt_created")]
        public DateTime CreatedAt { get; set; }
        
        [DataType(DataType.DateTime)]
        [Column("dt_update")]
        public DateTime UpdatedAt { get; set; }
 
    }
}
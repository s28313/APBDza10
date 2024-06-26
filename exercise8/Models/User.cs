using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kol2APBD.Models
{
    [Table("User")]
    public class User
    {
        [Key][Column("IdUser")]
        public int UserId { get; set; }
        
        [Column("Login")][MaxLength(100)]
        public string Login { get; set; }
        
        [Column("PasswordHash")][MaxLength(255)]
        public string PasswordHash { get; set; }
    }
}
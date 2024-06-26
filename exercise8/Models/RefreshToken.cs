using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kol2APBD.Models;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }
        
    public int UserId { get; set; }
        
    [Column(TypeName = "varchar(256)")]
    public string Token { get; set; }
        
    public DateTime Expires { get; set; }
}
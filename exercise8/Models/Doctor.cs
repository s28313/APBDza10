using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kol2APBD.Models;
[Table("Doctor")]
public class Doctor
{
    [Key][Column("IdDoctor")]
    public int DoctorId { get; set; }
    
    [Column("FirstName")][MaxLength(100)]
    public string DoctorFirstName { get; set; }
    
    [Column("LastName")][MaxLength(100)]
    public string DoctorLastName { get; set; }
    
    [Column("Email")][MaxLength(100)]
    public string DoctorEmail { get; set; }
    
    public IEnumerable<Prescription> Prescriptions { get; set; }
}
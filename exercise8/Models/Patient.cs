using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kol2APBD.Models;
[Table("Patient")]
public class Patient
{
    [Key][Column("IdPatient")]
    public int PatientId { get; set; }
    
    [Column("FirstName")][MaxLength(100)]
    public string PatientFirstName { get; set; }
    
    [Column("LastName")][MaxLength(100)]
    public string PatientLastName { get; set; }
    
    [Column("BirthDate")]
    public DateTime PatientBirthDate { get; set; }
    
    public IEnumerable<Prescription> Prescriptions { get; set; }
}
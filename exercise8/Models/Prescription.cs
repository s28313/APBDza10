using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kol2APBD.Models;
[Table("Prescription")]
public class Prescription
{
    [Key][Column("IdPrescription")]
    public int PrescriptionId { get; set;  }
    
    [Column("Date")]
    public DateTime PrescriptionDate { get; set;  }
    
    [Column("DueDate")]
    public DateTime PrescriptionDueDate { get; set; }
    
    [Column("IdPatient")][ForeignKey("Patient")]
    public int PatientId { get; set; }
    
    public Patient Patient { get; set; }
    
    [Column("IdDoctor")][ForeignKey("Doctor")]
    public int DoctorId { get; set; }
    
    public Doctor Doctor { get; set; }
    
    public IEnumerable<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
}
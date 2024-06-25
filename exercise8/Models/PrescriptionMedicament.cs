using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace kol2APBD.Models;
[Table("Prescription_Medicament")][PrimaryKey("MedicamentId","PrescriptionId")]
public class PrescriptionMedicament
{
    [Column("IdMedicament")][ForeignKey("Medicament")]
    public int MedicamentId { get; set; }
    
    public Medicament Medicament { get; set; }
    
    [Column("IdPrescription")][ForeignKey("Prescription")]
    public int PrescriptionId { get; set;  }
    
    public Prescription Prescription { get; set;  }
    
    [Column("Dose")]
    public int? PrescriptionMedicamentDose { get; set; }
    
    [Column("Details")][MaxLength(100)]
    public string PrescriptionMedicamentDetails { get; set; }
}
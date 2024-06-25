using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kol2APBD.Models;
[Table("Medicament")]
public class Medicament
{
    [Key][Column("IdMedicament")]
    public int MedicamentId { get; set; }
    
    [Column("Name")][MaxLength(100)]
    public string MedicamentName  { get; set; }
    
    [Column("Description")][MaxLength(100)]
    public string MedicamentDescription  { get; set; }
    
    [Column("Type")][MaxLength(100)]
    public string MedicamentType { get; set; }
    
    public IEnumerable<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
}
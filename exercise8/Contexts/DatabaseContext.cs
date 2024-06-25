using kol2APBD.Models;
using Microsoft.EntityFrameworkCore;

namespace kol2APBD.Contexts;

public class DatabaseContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor()
            {
                DoctorId = 1,
                DoctorFirstName = "Papaj",
                DoctorLastName = "Pawlikowski",
                DoctorEmail = "jp2@gmail.com"
            },
            new Doctor()
            {
                DoctorId = 2,
                DoctorFirstName = "Jan",
                DoctorLastName = "Kowalski",
                DoctorEmail = "jan.kowalski@onet.pl"
            }
        });
        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new Patient()
            {
                PatientId = 1,
                PatientFirstName = "Marek",
                PatientLastName = "Nowak",
                PatientBirthDate = new DateTime(2024,1,1)
                
            },
            new Patient()
            {
                PatientId = 2,
                PatientFirstName = "Teodor",
                PatientLastName = "Szeroki",
                PatientBirthDate = new DateTime(2000,1,1)
            }
        });
        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
        {
            new Prescription()
            {
                PrescriptionId = 1,
                PrescriptionDate = new DateTime(2023,1,1),
                PrescriptionDueDate = new DateTime(2024,6,1),
                PatientId = 1,
                DoctorId = 2
            },
            new Prescription()
            {
                PrescriptionId = 2,
                PrescriptionDate = new DateTime(2024,1,1),
                PrescriptionDueDate = new DateTime(2025,1,1),
                DoctorId = 2,
                PatientId = 1
            }
        });
        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament()
            {
                MedicamentId = 1,
                MedicamentName = "Amol",
                MedicamentDescription = "Na wszystko",
                MedicamentType = "Nie zaszkodzi a może pomoże"
            },
            new Medicament()
            {
                MedicamentId = 2,
                MedicamentName = "Fentanyl",
                MedicamentDescription = "Narkotyczny sen wywołuje",
                MedicamentType = "Silny opioid"
            }
        });
        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>()
        {
            new PrescriptionMedicament()
            {
                MedicamentId = 1,
                PrescriptionId = 1,
                PrescriptionMedicamentDose = 2137,
                PrescriptionMedicamentDetails = "Ile wlezie"
            },
            new PrescriptionMedicament()
            {
                MedicamentId = 2,
                PrescriptionId = 2,
                PrescriptionMedicamentDetails = "Byle nie zabić"
            }
        });
    }
}
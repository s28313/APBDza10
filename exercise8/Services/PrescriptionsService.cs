using kol2APBD.Contexts;
using kol2APBD.DTOs;
using kol2APBD.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace kol2APBD.Services;

public interface IPrescriptionsService
{
    Task<GetPrescriptionsResponseDTO> GetPrescription(int id);
}
public class PrescriptionsService(DatabaseContext context) : IPrescriptionsService
{
    public async Task<GetPrescriptionsResponseDTO> GetPrescription(int id)
    {
        var prescription = await context.Prescriptions.Where(e => e.PrescriptionId == id)
            .Include(e=>e.Patient)
            .Include(e=>e.Doctor)
            .FirstOrDefaultAsync();
        if (prescription is null)
        {
            throw new NotFoundException($"Prescription {id} not found");
        }
        return new GetPrescriptionsResponseDTO()
        {
            PatientFirstName = prescription.Patient.PatientFirstName,
            PatientLastName = prescription.Patient.PatientLastName,
            DoctorFirstName = prescription.Doctor.DoctorFirstName,
            DoctorLastName = prescription.Doctor.DoctorLastName,
            Date = prescription.PrescriptionDate,
            DueDate = prescription.PrescriptionDueDate,
            Drugs = await context.PrescriptionMedicaments.Where(e=>e.PrescriptionId==id)
                .Join(context.Medicaments,
                    presmed=>presmed.MedicamentId,med=>med.MedicamentId,
                    (presmed,med)=>new Drug(){
                    Name = med.MedicamentName,
                    Description = med.MedicamentDescription,
                    Type = med.MedicamentType,
                    Details = presmed.PrescriptionMedicamentDetails,
                    Dose = presmed.PrescriptionMedicamentDose
                }).ToListAsync()
        };
    }
}
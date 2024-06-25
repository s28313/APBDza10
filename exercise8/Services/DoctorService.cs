using kol2APBD.Contexts;
using kol2APBD.DTOs;
using kol2APBD.Exceptions;
using kol2APBD.Models;
using Microsoft.EntityFrameworkCore;

namespace kol2APBD.Services;

public interface IDoctorService
{
    Task<Doctor> GetDoc(int id);
    Task<int> AddDoc(PostDoctorRequestDTO data);
    Task ReplaceDoc(PutDoctorRequestDTO data);
    Task DeleteDoc(int id);
}

public class DoctorService(DatabaseContext context) : IDoctorService
{
    public async Task<Doctor> GetDoc(int id)
    {
        var doc = await context.Doctors.Where(e => e.DoctorId == id).FirstOrDefaultAsync();
        if (doc is null)
        {
            throw new NotFoundException($"Doctor {id} not found.");
        }
        return doc;
    }

    public async Task<int> AddDoc(PostDoctorRequestDTO data)
    {
        Doctor doctor = new Doctor()
        {
            DoctorFirstName = data.FirstName,
            DoctorLastName = data.LastName,
            DoctorEmail = data.Email
        };
        await context.Doctors.AddAsync(doctor);
        await context.SaveChangesAsync();
        return doctor.DoctorId;
    }

    public async Task ReplaceDoc(PutDoctorRequestDTO data)
    {
        var doctor = await GetDoc(data.Id);
        doctor.DoctorFirstName = data.FirstName;
        doctor.DoctorLastName = data.LastName;
        doctor.DoctorEmail = data.Email;
        context.Doctors.Update(doctor);
        await context.SaveChangesAsync(); 
    }

    public async Task DeleteDoc(int id)
    {
        var doctor = await GetDoc(id);
        await context.Doctors.Where(e=>e.DoctorId==id).ExecuteDeleteAsync();
        await context.SaveChangesAsync(); 
    }
}
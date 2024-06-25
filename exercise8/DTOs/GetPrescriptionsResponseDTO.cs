namespace kol2APBD.DTOs;

public class GetPrescriptionsResponseDTO
{
    public string PatientFirstName { get; set; }
    public string PatientLastName { get; set; }
    public string DoctorFirstName { get; set; }
    public string DoctorLastName { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<Drug> Drugs { get; set; }
}

public class Drug
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Details { get; set; }
    public int? Dose { get; set; }
}
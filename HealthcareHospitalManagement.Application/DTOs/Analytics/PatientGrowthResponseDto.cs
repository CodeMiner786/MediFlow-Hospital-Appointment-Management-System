namespace HealthcareHospitalManagement.Application.DTOs.Analytics;

public class PatientGrowthResponseDto
{
    public DateTime Date            { get; set; }
    public int      NewRegistrations { get; set; }
    public int      TotalPatients    { get; set; }
}

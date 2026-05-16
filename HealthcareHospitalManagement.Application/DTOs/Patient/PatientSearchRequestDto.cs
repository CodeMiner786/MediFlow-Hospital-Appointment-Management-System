using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Patient;

public class PatientSearchRequestDto
{
    public   string?    FirstName       { get; set; }
    public  string?     LastName        { get; set; }
    public  string?     PhoneNumber     { get; set; }
    public  string?     PatientCode     { get; set; }
    public  BloodGroup?  BloodGroup     { get; set; }
    public  int          PageNumber     { get; set; } = 1;
    public  int          PageSize       { get; set; } = 10;
}

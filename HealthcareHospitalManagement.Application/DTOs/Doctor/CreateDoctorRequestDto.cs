using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Doctor;

public class CreateDoctorRequestDto
{
    public Guid                  ApplicationUserId       { get; set; }
    public string                FirstName               { get; set; } = string.Empty;
    public string                LastName                { get; set; } = string.Empty;
    public DateTime              DateOfBirth             { get; set; }
    public Gender                Gender                  { get; set; }
    public DoctorSpecialization  Specialization          { get; set; }
    public DoctorSpecialization? SubSpecialization       { get; set; }
    public string                LicenseNumber           { get; set; } = string.Empty;
    public DateTime              LicenseExpiryDate       { get; set; }
    public int                   ExperienceYears         { get; set; }
    public string                Qualifications          { get; set; } = string.Empty;
    public decimal               ConsultationFee         { get; set; }
    public decimal?              TelemedicineConsultationFee { get; set; }
    public decimal?              HomeVisitFee            { get; set; }
    public string                PhoneNumber             { get; set; } = string.Empty;
    public string                Email                   { get; set; } = string.Empty;
    public Guid                  DepartmentId            { get; set; }
    public string?               Biography               { get; set; }
    public bool IsAvailableNow { get; set; } = false;

}

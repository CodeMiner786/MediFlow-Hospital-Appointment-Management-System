using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Doctor;

public class UpdateDoctorRequestDto
{
    public string?               Biography               { get; set; }
    public string?               PhoneNumber             { get; set; }
    public decimal?              ConsultationFee         { get; set; }
    public decimal?              TelemedicineConsultationFee { get; set; }
    public decimal?              HomeVisitFee            { get; set; }
    public string?               OfficeExtension         { get; set; }
    public DoctorSpecialization? SubSpecialization       { get; set; }
}

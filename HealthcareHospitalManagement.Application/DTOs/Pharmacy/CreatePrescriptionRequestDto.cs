using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;

namespace HealthcareHospitalManagement.Application.DTOs.Pharmacy;

public class CreatePrescriptionRequestDto
{
    public Guid     PatientId       { get; set; }
    public Guid     DoctorId        { get; set; }
    public Guid?    AppointmentId   { get; set; }
    public string   Diagnosis       { get; set; } = string.Empty;
    public string?  Notes           { get; set; }
    public List<PrescriptionItemRequestDto> Items { get; set; } = [];
}

using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Application.DTOs.Patient;

public class PatientVitalResponseDto
{
    public Guid     Id              { get; set; }
    public decimal? BloodPressureSystolic  { get; set; }
    public decimal? BloodPressureDiastolic { get; set; }
    public decimal? PulseRate       { get; set; }
    public decimal? Temperature     { get; set; }
    public decimal? OxygenSaturation { get; set; }
    public decimal? WeightKg        { get; set; }
    public decimal? HeightCm        { get; set; }
    public string?  Notes           { get; set; }
    public DateTime RecordedAt      { get; set; }
}

using HealthcareHospitalManagement.Domain.Enums.Lab;


namespace HealthcareHospitalManagement.Application.DTOs.Lab;

public class CreateLabOrderRequestDto
{
    public Guid         PatientId           { get; set; }
    public Guid?        OrderedByDoctorId   { get; set; }
    public Guid         LabProfileId        { get; set; }
    public TestPriority Priority            { get; set; } = TestPriority.Routine;
    public string?      ClinicalNotes       { get; set; }
    public string?      Diagnosis           { get; set; }
    public bool         IsFasting           { get; set; }
    public string?      SpecialInstructions { get; set; }
    public List<LabOrderItemRequestDto> Items { get; set; } = [];
}

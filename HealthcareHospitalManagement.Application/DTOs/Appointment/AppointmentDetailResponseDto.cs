using HealthcareHospitalManagement.Domain.Enums.Appointment;


namespace HealthcareHospitalManagement.Application.DTOs.Appointment;

public class AppointmentDetailResponseDto : AppointmentSummaryResponseDto
{
    public string   ReasonForVisit      { get; set; } = string.Empty;
    public string?  Symptoms            { get; set; }
    public string?  Notes               { get; set; }
    public bool     IsFirstVisit        { get; set; }
    public bool     IsFollowUp          { get; set; }
    public bool     ReminderSent        { get; set; }
    public string?  CancellationReason  { get; set; }
    public DateTime? CancelledAt        { get; set; }
    public string?  BookedBy            { get; set; }
}

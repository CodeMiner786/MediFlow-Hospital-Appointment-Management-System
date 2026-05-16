using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Notification;

namespace HealthcareHospitalManagement.Domain.Entities.Appointment
{
    /// <summary>
    /// Individual reminder logs for an appointment (SMS, Email, Push).
    /// Tracks scheduling, delivery status, and potential errors.
    /// </summary>
    public class AppointmentReminder : BaseEntity
    {
        // ── Foreign Keys & Navigation ──────────────────────────────────────────────────────────────────
        public Guid AppointmentId { get; set; }
        public AppointmentEntity Appointment { get; set; } = null!;

        // ── Notification Details ───────────────────────────────────────────────────────────────────────
        public NotificationChannel Channel { get; set; }
        public NotificationStatus Status { get; set; } = NotificationStatus.Pending;

        // ── Scheduling & Logs ──────────────────────────────────────────────────────────────────────────
        public DateTime ScheduledAt { get; set; }
        public DateTime? SentAt { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
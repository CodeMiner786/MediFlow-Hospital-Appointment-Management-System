using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Billing;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.Appointment;

namespace HealthcareHospitalManagement.Domain.Entities.Appointment
{
    /// <summary>
    /// Represents a scheduled medical consultation between a patient and a doctor.
    /// Supports self-referencing for follow-up appointments.
    /// </summary>
    public class AppointmentEntity : BaseEntity
    {
        // ── Identification ─────────────────────────────────────────────────────────────────────────────
        public    string                AppointmentCode         { get; set; } = string.Empty;

        // ── Foreign Keys & Navigation ──────────────────────────────────────────────────────────────────
        public    Guid                  PatientId               { get; set; }
        public    Patient               Patient                 { get; set; } = null!;

        public    Guid                  DoctorId                { get; set; }
        public    DoctorEntity          Doctor                  { get; set; } = null!;

        public    Guid?                 BillId                  { get; set; }
        public    Bill?                 Bill                    { get; set; }

        // ── Schedule Details ───────────────────────────────────────────────────────────────────────────
        public    DateTime              AppointmentDate         { get; set; }
        public    TimeOnly              StartTime               { get; set; }
        public    TimeOnly              EndTime                 { get; set; }
        public    AppointmentType       AppointmentType         { get; set; }
        public    AppointmentStatus     Status                  { get; set; } = AppointmentStatus.Scheduled;

        // ── Clinical Info ──────────────────────────────────────────────────────────────────────────────
        public    string                ReasonForVisit          { get; set; } = string.Empty;
        public    string?               Symptoms                { get; set; }
        public    string?               Notes                   { get; set; }
        public    bool                  IsFirstVisit            { get; set; }
        public    bool                  IsFollowUp              { get; set; }

        // ── Self-Referencing (Follow-up Tracking) ──────────────────────────────────────────────────────
        public    Guid?                 PreviousAppointmentId   { get; set; }
        public    AppointmentEntity?    PreviousAppointment     { get; set; }

        // ── Reminders & Logs ───────────────────────────────────────────────────────────────────────────
        public    bool                  ReminderSent            { get; set; } = false;
        public    DateTime?             ReminderSentAt          { get; set; }
        public    string?               BookedBy                { get; set; }
        public    bool                  IsManualBooking         { get; set; } = false;

        // ── Payment & Cancellation ─────────────────────────────────────────────────────────────────────
        public    bool                  IsPaid                  { get; set; } = false;
        public    string?               CancellationReason      { get; set; }
        public    DateTime?             CancelledAt             { get; set; }
        public    string?               CancelledByUserId       { get; set; }
    }
}
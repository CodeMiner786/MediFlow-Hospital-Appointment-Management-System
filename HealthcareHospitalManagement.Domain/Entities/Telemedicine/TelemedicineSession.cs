using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Appointment;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.Telemedicine;

namespace HealthcareHospitalManagement.Domain.Entities.Telemedicine
{
    public class TelemedicineSession : BaseEntity
    {
        // ——— ১. আইডেন্টিফিকেশন ও রিলেশনস ———
        public      string              SessionCode             { get; set; } = string.Empty;

        public      Guid                AppointmentId           { get; set; }
        public      AppointmentEntity   Appointment             { get; set; } = null!;

        public      Guid                PatientId               { get; set; }
        public      Patient             Patient                 { get; set; } = null!;

        public      Guid                DoctorId                { get; set; }
        public      DoctorEntity        Doctor                  { get; set; } = null!;


        // ——— ২. ভিডিও কনফারেন্সিং ডিটেইলস ———
        public      VideoCallProvider   Provider                { get; set; }
        public      TelemedicineSessionStatus Status            { get; set; } = TelemedicineSessionStatus.Scheduled;

        public      string?             RoomId                  { get; set; }
        public      string?             MeetingLink             { get; set; }
        public      string?             PatientToken            { get; set; }
        public      string?             DoctorToken             { get; set; }


        // ——— ৩. টাইমিং ও ডিউরেশন ———
        public      DateTime            ScheduledAt             { get; set; }
        public      DateTime?           StartedAt               { get; set; }
        public      DateTime?           EndedAt                 { get; set; }
        public      int?                DurationMinutes         { get; set; }


        // ——— ৪. ক্লিনিক্যাল নোটস ———
        public      string?             DoctorNotes             { get; set; }
        public      string?             Prescription            { get; set; }
        public      string?             FollowUpInstructions    { get; set; }
        public      DateTime?           FollowUpDate            { get; set; }


        // ——— ৫. রেকর্ডিং ———
        public      bool                IsRecorded              { get; set; } = false;
        public      string?             RecordingUrl            { get; set; }
    }
}
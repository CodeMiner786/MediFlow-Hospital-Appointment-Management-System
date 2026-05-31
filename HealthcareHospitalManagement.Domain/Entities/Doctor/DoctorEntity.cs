using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Appointment;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using HealthcareHospitalManagement.Domain.Enums.Patient;

namespace HealthcareHospitalManagement.Domain.Entities.Doctor;

public class DoctorEntity : BaseEntity
{
    // ১. আইডেন্টিটি এবং বেসিক ইনফো
    public Guid ApplicationUserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"Dr. {FirstName} {LastName}";
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? Biography { get; set; }

    // ২. প্রফেশনাল ডিটেইলস
    public string DoctorCode { get; set; } = string.Empty;
    public DoctorSpecialization Specialization { get; set; }
    public DoctorSpecialization? SubSpecialization { get; set; }
    public string LicenseNumber { get; set; } = string.Empty;
    public DateTime LicenseExpiryDate { get; set; }
    public int ExperienceYears { get; set; }
    public string Qualifications { get; set; } = string.Empty;

    // ৩. ফিন্যান্সিয়াল
    public decimal ConsultationFee { get; set; }
    public decimal? TelemedicineConsultationFee { get; set; }
    public decimal? HomeVisitFee { get; set; }
    public decimal? CustomDoctorSharePercent { get; set; }

    // ৪. স্ট্যাটাস এবং কন্টাক্ট
    public DoctorAvailabilityStatus AvailabilityStatus { get; set; } = DoctorAvailabilityStatus.Offline;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? OfficeExtension { get; set; }

    public bool IsAvailableNow { get; set; } = false;



    // ৫. পারফরম্যান্স এবং ডিপার্টমেন্ট
    public decimal AverageRating { get; set; } = 0;
    public int TotalReviews { get; set; } = 0;
    public int TotalAppointments { get; set; } = 0;
    public Guid DepartmentId { get; set; }
    public DepartmentEntity Department { get; set; } = null!;

    // ৬. নেভিগেশন কালেকশন
    public virtual ICollection<DoctorSchedule> Schedules { get; set; } = [];
    public virtual ICollection<DoctorScheduleSlot> ScheduleSlots { get; set; } = [];
    public virtual ICollection<DoctorLeave> Leaves { get; set; } = [];
    public virtual ICollection<DoctorUnavailability> Unavailabilities { get; set; } = [];
    public virtual ICollection<DoctorAvailabilityLog> AvailabilityLogs { get; set; } = [];
    public virtual ICollection<AppointmentEntity> Appointments { get; set; } = [];
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = [];
    public virtual ICollection<DoctorNote> Notes { get; set; } = [];
    
    // এই দুটি কালেকশন এখন Configuration ফাইলের সাথে ম্যাপিং করা
    public virtual ICollection<PatientReferral> ReferralsMade { get; set; } = [];
    public virtual ICollection<PatientReferral> ReferralsReceived { get; set; } = [];
    
    public virtual ICollection<DoctorEarning> Earnings { get; set; } = [];
    public virtual ICollection<DoctorDocument> Documents { get; set; } = [];
    public virtual ICollection<DoctorPerformanceReport> PerformanceReports { get; set; } = [];
    public virtual DoctorFeedbackSummary? FeedbackSummary { get; set; }
}
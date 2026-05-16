using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.Entities.Analytics;
using HealthcareHospitalManagement.Domain.Entities.Appointment;
using HealthcareHospitalManagement.Domain.Entities.Billing;
using HealthcareHospitalManagement.Domain.Entities.Chat;
using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Entities.Emergency;
using HealthcareHospitalManagement.Domain.Entities.Feed;
using HealthcareHospitalManagement.Domain.Entities.Feedback;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.Entities.Notification;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Entities.Payment;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Entities.Telemedicine;
using HealthcareHospitalManagement.Domain.Entities.Wards;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HealthcareHospitalManagement.Application.Interfaces;

public interface IApplicationDbContext
{
    // ── Identity ─────────────────────────────────────────────────────────
    DbSet<ApplicationUser> ApplicationUsers { get; }
    DbSet<OtpCode> OtpCodes { get; }
    DbSet<PasswordResetRequest> PasswordResetRequests { get; }
    DbSet<UserLoginHistory> UserLoginHistories { get; }
    DbSet<UserNotification> UserNotifications { get; }
    DbSet<UserPermissionOverride> UserPermissionOverrides { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<RoleAssignmentLog> RoleAssignmentLogs { get; }
    DbSet<AuditLog> AuditLogs { get; }

    // ── Refresh Tokens ────────────────────────────────────────────────────
    DbSet<Domain.Entities.Identity.UserRefreshToken> UserRefreshTokens { get; } // ✅ RefreshTokens namespace

    // ── Patient ───────────────────────────────────────────────────────────
    DbSet<Patient> Patients { get; }
    DbSet<PatientVital> PatientVitals { get; }
    DbSet<HealthLog> HealthLogs { get; }
    DbSet<MedicalRecord> MedicalRecords { get; }

    // ── Doctor ────────────────────────────────────────────────────────────
    DbSet<DoctorEntity> Doctors { get; }
    DbSet<Department> Departments { get; }
    DbSet<StaffEntity> Staff { get; }
    DbSet<StaffAttendance> StaffAttendances { get; }
    DbSet<DoctorSchedule> DoctorSchedules { get; }
    DbSet<DoctorScheduleSlot> DoctorScheduleSlots { get; }
    DbSet<DoctorLeave> DoctorLeaves { get; }
    DbSet<DoctorUnavailability> DoctorUnavailabilities { get; }
    DbSet<DoctorAvailabilityLog> DoctorAvailabilityLogs { get; }
    DbSet<DoctorNote> DoctorNotes { get; }
    DbSet<DoctorEarning> DoctorEarnings { get; }
    DbSet<DoctorDocument> DoctorDocuments { get; }
    DbSet<DoctorFeedbackSummary> DoctorFeedbackSummaries { get; }
    DbSet<DoctorPerformanceReport> DoctorPerformanceReports { get; }
    DbSet<DoctorNotification> DoctorNotifications { get; }
    DbSet<PatientReferral> PatientReferrals { get; }
    DbSet<HospitalSettings> HospitalSettings { get; }

    // ── Appointment ───────────────────────────────────────────────────────
    DbSet<AppointmentEntity> Appointments { get; }
    DbSet<AppointmentReminder> AppointmentReminders { get; }

    // ── Billing ───────────────────────────────────────────────────────────
    DbSet<Bill> Bills { get; }
    DbSet<BillItem> BillItems { get; }
    DbSet<Invoice> Invoices { get; }
    DbSet<InsuranceClaim> InsuranceClaims { get; }

    // ── Payment ───────────────────────────────────────────────────────────
    DbSet<PaymentTransaction> PaymentTransactions { get; }
    DbSet<PlatformWallet> PlatformWallets { get; }
    DbSet<PlatformWalletTransaction> PlatformWalletTransactions { get; }

    // ── Ambulance ─────────────────────────────────────────────────────────
    DbSet<AmbulanceProviderProfile> AmbulanceProviderProfiles { get; }
    DbSet<AmbulanceVehicle> AmbulanceVehicles { get; }
    DbSet<AmbulanceBooking> AmbulanceBookings { get; }
    DbSet<AmbulanceServiceListing> AmbulanceServiceListings { get; }
    DbSet<AmbulanceProviderWallet> AmbulanceProviderWallets { get; }
    DbSet<AmbulanceWalletTransaction> AmbulanceWalletTransactions { get; }

    // ── Lab ───────────────────────────────────────────────────────────────
    DbSet<LabProfile> LabProfiles { get; }
    DbSet<LabTest> LabTests { get; }
    DbSet<LabOrder> LabOrders { get; }
    DbSet<LabOrderItem> LabOrderItems { get; }
    DbSet<LabServiceListing> LabServiceListings { get; }

    // ── Pharmacy ──────────────────────────────────────────────────────────
    DbSet<PharmacyProfile> PharmacyProfiles { get; }
    DbSet<Medicine> Medicines { get; }
    DbSet<MedicineStock> MedicineStocks { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<Prescription> Prescriptions { get; }
    DbSet<PrescriptionItem> PrescriptionItems { get; }
    DbSet<MedicineOrder> MedicineOrders { get; }
    DbSet<MedicineOrderItem> MedicineOrderItems { get; }
    DbSet<PharmacyServiceListing> PharmacyServiceListings { get; }

    // ── Ward ──────────────────────────────────────────────────────────────
    DbSet<Ward> Wards { get; }
    DbSet<Bed> Beds { get; }
    DbSet<Admission> Admissions { get; }
    DbSet<BedBooking> BedBookings { get; }

    // ── Emergency ─────────────────────────────────────────────────────────
    DbSet<EmergencyVisit> EmergencyVisits { get; }

    // ── Telemedicine ──────────────────────────────────────────────────────
    DbSet<TelemedicineSession> TelemedicineSessions { get; }

    // ── Chat ──────────────────────────────────────────────────────────────
    DbSet<Conversation> Conversations { get; }
    DbSet<ChatMessage> ChatMessages { get; }

    // ── Feed ──────────────────────────────────────────────────────────────
    DbSet<FeedItem> FeedItems { get; }
    DbSet<FeedItemTag> FeedItemTags { get; }
    DbSet<FeedItemLike> FeedItemLikes { get; }
    DbSet<FeedItemSave> FeedItemSaves { get; }

    // ── Feedback ──────────────────────────────────────────────────────────
    DbSet<FeedbackEntity> Feedbacks { get; }

    // ── Notification ──────────────────────────────────────────────────────
    DbSet<NotificationTemplate> NotificationTemplates { get; }

    // ── Analytics ─────────────────────────────────────────────────────────
    DbSet<DashboardMetric> DashboardMetrics { get; }
    DbSet<ReportEntity> Reports { get; }

    // ── Dashboard ─────────────────────────────────────────────────────────
    DbSet<AdminDashboardMetric> AdminDashboardMetrics { get; }
    DbSet<DoctorDashboardMetric> DoctorDashboardMetrics { get; }
    DbSet<AmbulanceDashboardMetric> AmbulanceDashboardMetrics { get; }
    DbSet<LabDashboardMetric> LabDashboardMetrics { get; }
    DbSet<PharmacyDashboardMetric> PharmacyDashboardMetrics { get; }

    // ── Save ──────────────────────────────────────────────────────────────
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
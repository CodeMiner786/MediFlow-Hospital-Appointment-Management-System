using HealthcareHospitalManagement.Domain.Interfaces.Ambulance;
using HealthcareHospitalManagement.Domain.Interfaces.Analytics;
using HealthcareHospitalManagement.Domain.Interfaces.Appointment;
using HealthcareHospitalManagement.Domain.Interfaces.Billing;
using HealthcareHospitalManagement.Domain.Interfaces.Chat;
using HealthcareHospitalManagement.Domain.Interfaces.Dashboard;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using HealthcareHospitalManagement.Domain.Interfaces.Emergency;
using HealthcareHospitalManagement.Domain.Interfaces.Feed;
using HealthcareHospitalManagement.Domain.Interfaces.Feed.Posts;
using HealthcareHospitalManagement.Domain.Interfaces.Feedback;
using HealthcareHospitalManagement.Domain.Interfaces.Identity;
using HealthcareHospitalManagement.Domain.Interfaces.Lab;
using HealthcareHospitalManagement.Domain.Interfaces.Notification;
using HealthcareHospitalManagement.Domain.Interfaces.Patients;
using HealthcareHospitalManagement.Domain.Interfaces.Payment;
using HealthcareHospitalManagement.Domain.Interfaces.Pharmacy;
using HealthcareHospitalManagement.Domain.Interfaces.Telemedicine;
using HealthcareHospitalManagement.Domain.Interfaces.Wards;
using HealthcareHospitalManagement.Infrastructure.Repositories.Ambulance;
using HealthcareHospitalManagement.Infrastructure.Repositories.Analytics;
using HealthcareHospitalManagement.Infrastructure.Repositories.Appointment;
using HealthcareHospitalManagement.Infrastructure.Repositories.Billing;
using HealthcareHospitalManagement.Infrastructure.Repositories.Chat;
using HealthcareHospitalManagement.Infrastructure.Repositories.Dashboard;
using HealthcareHospitalManagement.Infrastructure.Repositories.Doctor;
using HealthcareHospitalManagement.Infrastructure.Repositories.Emergency;
using HealthcareHospitalManagement.Infrastructure.Repositories.Feed;
using HealthcareHospitalManagement.Infrastructure.Repositories.Feed.Posts;
using HealthcareHospitalManagement.Infrastructure.Repositories.Feedback;
using HealthcareHospitalManagement.Infrastructure.Repositories.Identity;
using HealthcareHospitalManagement.Infrastructure.Repositories.Lab;
using HealthcareHospitalManagement.Infrastructure.Repositories.Notification;
using HealthcareHospitalManagement.Infrastructure.Repositories.Patients;
using HealthcareHospitalManagement.Infrastructure.Repositories.Payment;
using HealthcareHospitalManagement.Infrastructure.Repositories.Pharmacy;
using HealthcareHospitalManagement.Infrastructure.Repositories.Telemedicine;
using HealthcareHospitalManagement.Infrastructure.Repositories.Wards;
using Microsoft.Extensions.DependencyInjection;



namespace HealthcareHospitalManagement.Infrastructure.ServiceExtensions.RepositoryRegistration;

public static class RepositoryServiceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // =============================================
        // Identity & Auth (10)
        // =============================================
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOtpRepository, OtpRepository>();
        services.AddScoped<IPasswordResetRepository, PasswordResetRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<IRoleAssignmentLogRepository, RoleAssignmentLogRepository>();
        services.AddScoped<IUserLoginHistoryRepository, UserLoginHistoryRepository>();
        services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
        services.AddScoped<IUserPermissionOverrideRepository, UserPermissionOverrideRepository>();
        services.AddScoped<IUserRefreshTokenRepository,UserRefreshTokenRepository>();

        // =============================================
        // Doctor (16)
        // =============================================
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IDoctorAvailabilityLogRepository, DoctorAvailabilityLogRepository>();
        services.AddScoped<IDoctorDocumentRepository, DoctorDocumentRepository>();
        services.AddScoped<IDoctorEarningRepository, DoctorEarningRepository>();
        services.AddScoped<IDoctorFeedbackSummaryRepository, DoctorFeedbackSummaryRepository>();
        services.AddScoped<IDoctorLeaveRepository, DoctorLeaveRepository>();
        services.AddScoped<IDoctorNoteRepository, DoctorNoteRepository>();
        services.AddScoped<IDoctorNotificationRepository, DoctorNotificationRepository>();
        services.AddScoped<IDoctorPerformanceReportRepository, DoctorPerformanceReportRepository>();
        services.AddScoped<IDoctorScheduleRepository, DoctorScheduleRepository>();
        services.AddScoped<IDoctorScheduleSlotRepository, DoctorScheduleSlotRepository>();
        services.AddScoped<IDoctorUnavailabilityRepository, DoctorUnavailabilityRepository>();
        services.AddScoped<IHospitalSettingsRepository, HospitalSettingsRepository>();
        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddScoped<IStaffAttendanceRepository, StaffAttendanceRepository>();

        // =============================================
        // Patient (5)
        // =============================================
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IHealthLogRepository, HealthLogRepository>();
        services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
        services.AddScoped<IPatientReferralRepository, PatientReferralRepository>();
        services.AddScoped<IPatientVitalRepository, PatientVitalRepository>();

        // =============================================
        // Appointment (2)
        // =============================================
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IAppointmentReminderRepository, AppointmentReminderRepository>();

        // =============================================
        // Lab (5)
        // =============================================
        services.AddScoped<ILabOrderRepository, LabOrderRepository>();
        services.AddScoped<ILabOrderItemRepository, LabOrderItemRepository>();
        services.AddScoped<ILabProfileRepository, LabProfileRepository>();
        services.AddScoped<ILabServiceListingRepository, LabServiceListingRepository>();
        services.AddScoped<ILabTestRepository, LabTestRepository>();

        // =============================================
        // Pharmacy (9)
        // =============================================
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        services.AddScoped<IPrescriptionItemRepository, PrescriptionItemRepository>();
        services.AddScoped<IMedicineRepository, MedicineRepository>();
        services.AddScoped<IMedicineOrderRepository, MedicineOrderRepository>();
        services.AddScoped<IMedicineOrderItemRepository, MedicineOrderItemRepository>();
        services.AddScoped<IMedicineStockRepository, MedicineStockRepository>();
        services.AddScoped<IPharmacyProfileRepository, PharmacyProfileRepository>();
        services.AddScoped<IPharmacyServiceListingRepository, PharmacyServiceListingRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();

        // =============================================
        // Ambulance (4)
        // =============================================
        services.AddScoped<IAmbulanceBookingRepository, AmbulanceBookingRepository>();
        services.AddScoped<IAmbulanceProviderProfileRepository, AmbulanceProviderProfileRepository>();
        services.AddScoped<IAmbulanceProviderWalletRepository, AmbulanceProviderWalletRepository>();
        services.AddScoped<IAmbulanceServiceListingRepository, AmbulanceServiceListingRepository>();

        // =============================================
        // Billing & Payment (7)
        // =============================================
        services.AddScoped<IBillRepository, BillRepository>();
        services.AddScoped<IBillItemRepository, BillItemRepository>();
        services.AddScoped<IInsuranceClaimRepository, InsuranceClaimRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();
        services.AddScoped<IPlatformWalletRepository, PlatformWalletRepository>();
        services.AddScoped<IPlatformWalletTransactionRepository, PlatformWalletTransactionRepository>();

        // =============================================
        // Ward & Emergency (5)
        // =============================================
        services.AddScoped<IWardRepository, WardRepository>();
        services.AddScoped<IBedRepository, BedRepository>();
        services.AddScoped<IBedBookingRepository, BedBookingRepository>();
        services.AddScoped<IAdmissionRepository, AdmissionRepository>();
        services.AddScoped<IEmergencyVisitRepository, EmergencyVisitRepository>();

        // =============================================
        // Telemedicine (1)
        // =============================================
        services.AddScoped<ITelemedicineSessionRepository, TelemedicineSessionRepository>();

        // =============================================
        // Chat & Feed (17)
        // =============================================
        services.AddScoped<IConversationRepository, ConversationRepository>();
        services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
        services.AddScoped<IFeedItemRepository, FeedItemRepository>();
        services.AddScoped<IFeedItemLikeRepository, FeedItemLikeRepository>();
        services.AddScoped<IFeedItemSaveRepository, FeedItemSaveRepository>();
        services.AddScoped<IFeedItemTagRepository, FeedItemTagRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostAudienceRepository, PostAudienceRepository>();
        services.AddScoped<IPostCommentRepository, PostCommentRepository>();
        services.AddScoped<IPostContentRepository, PostContentRepository>();
        services.AddScoped<IPostInteractionRepository, PostInteractionRepository>();
        services.AddScoped<IPostLikeRepository, PostLikeRepository>();
        services.AddScoped<IPostMediaRepository, PostMediaRepository>();
        services.AddScoped<IPostMetaRepository, PostMetaRepository>();
        services.AddScoped<IPostSaveRepository, PostSaveRepository>();
        services.AddScoped<IPostShareRepository, PostShareRepository>();
        services.AddScoped<IPostTagRepository, PostTagRepository>();

        // =============================================
        // Dashboard (5)
        // =============================================
        services.AddScoped<IAdminDashboardMetricRepository, AdminDashboardMetricRepository>();
        services.AddScoped<IAmbulanceDashboardMetricRepository, AmbulanceDashboardMetricRepository>();
        services.AddScoped<IDoctorDashboardMetricRepository, DoctorDashboardMetricRepository>();
        services.AddScoped<ILabDashboardMetricRepository, LabDashboardMetricRepository>();
        services.AddScoped<IPharmacyDashboardMetricRepository, PharmacyDashboardMetricRepository>();

        // =============================================
        // Analytics & Notification & Feedback (4)
        // =============================================
        services.AddScoped<IDashboardMetricRepository, DashboardMetricRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<INotificationTemplateRepository, NotificationTemplateRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();

        return services;
    }
}

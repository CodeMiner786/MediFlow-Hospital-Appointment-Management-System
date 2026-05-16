using AutoMapper;


// ── Entities ───────────────────────────────────────────────────────────────────
using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.Entities.Analytics;
using HealthcareHospitalManagement.Domain.Entities.Appointment;
using HealthcareHospitalManagement.Domain.Entities.Billing;
using HealthcareHospitalManagement.Domain.Entities.Chat;
using HealthcareHospitalManagement.Domain.Entities.Dashboard;
using HealthcareHospitalManagement.Domain.Entities.Doctor;
using HealthcareHospitalManagement.Domain.Entities.Emergency;
using HealthcareHospitalManagement.Domain.Entities.Feed;
using HealthcareHospitalManagement.Domain.Entities.Feed.Posts;
using HealthcareHospitalManagement.Domain.Entities.Feedback;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.Entities.Notification;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Entities.Payment;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Entities.Telemedicine;
using HealthcareHospitalManagement.Domain.Entities.Wards;
using HealthcareHospitalManagement.Application.DTOs.Patient;
using HealthcareHospitalManagement.Application.DTOs.Appointment;
using HealthcareHospitalManagement.Application.DTOs.Ambulance;
using HealthcareHospitalManagement.Application.DTOs.Billing;
using HealthcareHospitalManagement.Application.DTOs.Dashboard;
using HealthcareHospitalManagement.Application.DTOs.Doctor;
using HealthcareHospitalManagement.Application.DTOs.Emergency;
using HealthcareHospitalManagement.Application.DTOs.Feed;
using HealthcareHospitalManagement.Application.DTOs.Feedback;
using HealthcareHospitalManagement.Application.DTOs.Lab;
using HealthcareHospitalManagement.Application.DTOs.Notification;
using HealthcareHospitalManagement.Application.DTOs.Payment;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Application.DTOs.Telemedicine;
using HealthcareHospitalManagement.Application.DTOs.Wards;
using HealthcareHospitalManagement.Application.DTOs.Chat;
using HealthcareHospitalManagement.Application.DTOs.Auth;
using HealthcareHospitalManagement.Application.DTOs.Identity;
using HealthcareHospitalManagement.Application.DTOs.Analytics;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;


namespace HealthcareHospitalManagement.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        // ApplicationUser -> AuthResponseDto (লগইন এবং রিফ্রেশ টোকেনের জন্য)
        CreateMap<ApplicationUser, AuthResponseDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

        // ApplicationUser -> UserProfileResponseDto (প্রোফাইলের জন্য)
        CreateMap<ApplicationUser, UserProfileResponseDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));

        // RegisterRequestDto -> ApplicationUser (রেজিস্ট্রেশনের জন্য)
        CreateMap<RegisterRequestDto, ApplicationUser>();

        // Identity Logs
        CreateMap<AuditLog, AuditLogResponseDto>();
        CreateMap<RoleAssignmentLog, RoleAssignmentLogResponseDto>();
        CreateMap<UserLoginHistory, UserLoginHistoryResponseDto>();


        // ════════════════════════════════════════════════════════════════
        // AMBULANCE
        // ════════════════════════════════════════════════════════════════
        CreateMap<AmbulanceBooking, AmbulanceBookingResponseDto>();
        CreateMap<BookAmbulanceRequestDto, AmbulanceBooking>();
        CreateMap<UpdateAmbulanceBookingStatusRequestDto, AmbulanceBooking>();
        CreateMap<AmbulanceProviderProfile, AmbulanceProviderResponseDto>();
        CreateMap<AmbulanceVehicle, AmbulanceVehicleResponseDto>();


        // ════════════════════════════════════════════════════════════════
        // ANALYTICS & DASHBOARD
        // ════════════════════════════════════════════════════════════════
        CreateMap<DashboardMetric, DashboardMetricResponseDto>();
        CreateMap<ReportEntity, RevenueReportResponseDto>();
        CreateMap<AdminDashboardMetric, AdminDashboardResponseDto>();
        CreateMap<AmbulanceDashboardMetric, AmbulanceDashboardResponseDto>();
        CreateMap<DoctorDashboardMetric, DoctorDashboardResponseDto>();
        CreateMap<LabDashboardMetric, LabDashboardResponseDto>();
        CreateMap<PharmacyDashboardMetric, PharmacyDashboardResponseDto>();


        // ════════════════════════════════════════════════════════════════
        // APPOINTMENT
        // ════════════════════════════════════════════════════════════════
        CreateMap<AppointmentEntity, AppointmentSummaryResponseDto>();
        CreateMap<AppointmentEntity, AppointmentDetailResponseDto>();
        CreateMap<AppointmentEntity, TodayQueueItemResponseDto>();
        CreateMap<BookAppointmentRequestDto, AppointmentEntity>();
        CreateMap<AppointmentReminder, AppointmentFilterRequestDto>();
        CreateMap<CancelAppointmentRequestDto, AppointmentEntity>()
            .ForMember(dest => dest.CancellationReason, opt => opt.MapFrom(src => src.CancellationReason))
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // AppointmentId দিয়ে entity খুঁজে আনা হবে

        CreateMap<RescheduleAppointmentRequestDto, AppointmentEntity>()
            .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.NewDate))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.NewStartTime))
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // AppointmentId দিয়ে entity খুঁজে আনা হবে



        // ════════════════════════════════════════════════════════════════
        // BILLING
        // ════════════════════════════════════════════════════════════════
        CreateMap<Bill, BillSummaryResponseDto>();
        CreateMap<Bill, BillDetailResponseDto>();
        CreateMap<CreateBillRequestDto, Bill>();
        CreateMap<BillItem, BillItemResponseDto>();
        CreateMap<BillItemRequestDto, BillItem>();
        CreateMap<Invoice, InvoiceResponseDto>();
        CreateMap<InsuranceClaim, InsuranceClaimResponseDto>();


        // CHAT
        // ════════════════════════════════════════════════════════════════
        CreateMap<ChatMessage, ChatMessageResponseDto>();
        CreateMap<SendMessageRequestDto, ChatMessage>();
        CreateMap<Conversation, ConversationResponseDto>();
        CreateMap<StartConversationRequestDto, Conversation>();


        // ════════════════════════════════════════════════════════════════
        // DOCTOR
        // ════════════════════════════════════════════════════════════════
        CreateMap<DoctorEntity, DoctorSummaryResponseDto>();
        CreateMap<DoctorEntity, DoctorDetailResponseDto>();
        CreateMap<CreateDoctorRequestDto, DoctorEntity>();

        // 🔎 UpdateDoctorRequestDto → DoctorEntity mapping
        CreateMap<UpdateDoctorRequestDto, DoctorEntity>()
            .ForAllMembers(opts =>
             opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Department, DepartmentResponseDto>();
        CreateMap<DoctorScheduleRequestDto, DoctorSchedule>();
        CreateMap<DoctorScheduleSlot, DoctorScheduleSlotResponseDto>();
        CreateMap<DoctorLeaveRequestDto, DoctorLeave>();


        // ════════════════════════════════════════════════════════════════
        // EMERGENCY
        // ════════════════════════════════════════════════════════════════
        CreateMap<EmergencyVisit, EmergencyVisitResponseDto>();
        CreateMap<CreateEmergencyVisitRequestDto, EmergencyVisit>();
        CreateMap<UpdateEmergencyVisitRequestDto, EmergencyVisit>();


        // ════════════════════════════════════════════════════════════════
        // FEED — FeedItem
        // ════════════════════════════════════════════════════════════════
        CreateMap<FeedItem, FeedItemResponseDto>();
        CreateMap<CreateFeedItemRequestDto, FeedItem>();
        CreateMap<UpdateFeedItemRequestDto, FeedItem>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<FeedFilterRequestDto, FeedItem>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<FeedItemLike, FeedItemLikeRequestDto>().ReverseMap();
        CreateMap<FeedItemSave, FeedItemSaveRequestDto>().ReverseMap();
        CreateMap<CreateFeedPostRequestDto, FeedItem>();


        // ════════════════════════════════════════════════════════════════
        // FEED — Posts
        // ════════════════════════════════════════════════════════════════
        CreateMap<Post, PostResponseDto>()
            .ForMember(dest => dest.AuthorName,
                opt => opt.MapFrom(src => $"{src.AuthorUser.FirstName} {src.AuthorUser.LastName}"))
            .ForMember(dest => dest.AuthorImageUrl,
                opt => opt.MapFrom(src => src.AuthorUser.ProfileImageUrl))
            .ForMember(dest => dest.Content,
                opt => opt.MapFrom(src => src.Content.TextBody))
            .ForMember(dest => dest.Audience,
                opt => opt.MapFrom(src => src.Audience.Visibility.ToString()))
            .ForMember(dest => dest.LikeCount,
                opt => opt.MapFrom(src => src.Interactions.Likes.Count))
            .ForMember(dest => dest.CommentCount,
                opt => opt.MapFrom(src => src.Interactions.Comments.Count))
            .ForMember(dest => dest.ShareCount,
                opt => opt.MapFrom(src => src.Interactions.Shares.Count))
            .ForMember(dest => dest.MediaUrls,
                opt => opt.MapFrom(src => src.MediaFiles.Select(m => m.MediaUrl).ToList()))
            .ForMember(dest => dest.Tags,
                opt => opt.MapFrom(src => src.Tags.Select(t => t.Tag).ToList()));

        CreateMap<CreateFeedPostRequestDto, Post>();
        CreateMap<UpdatePostRequestDto, Post>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<PostFilterRequestDto, Post>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<PostComment, PostCommentResponseDto>();
        CreateMap<AddPostCommentRequestDto, PostComment>();
        CreateMap<PostLike, PostLikeRequestDto>().ReverseMap();
        CreateMap<PostSave, PostSaveRequestDto>().ReverseMap();
        CreateMap<PostShare, PostShareRequestDto>().ReverseMap();

        CreateMap<PostInteraction, PostInteractionSummaryDto>()
            .ForMember(dest => dest.PostId,
                opt => opt.MapFrom(src => src.PostId))
            .ForMember(dest => dest.LikeCount,
                opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.CommentCount,
                opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.ShareCount,
                opt => opt.MapFrom(src => src.Shares.Count))
            .ForMember(dest => dest.SaveCount,
                opt => opt.MapFrom(src => src.Saves.Count));


        // ════════════════════════════════════════════════════════════════
        // FEEDBACK
        // ════════════════════════════════════════════════════════════════
        CreateMap<FeedbackEntity, FeedbackResponseDto>();
        CreateMap<FeedbackEntity, FeedbackSummaryResponseDto>();
        CreateMap<CreateFeedbackRequestDto, FeedbackEntity>();


        // ════════════════════════════════════════════════════════════════
        // LAB
        // ════════════════════════════════════════════════════════════════
        CreateMap<LabOrder, LabOrderResponseDto>();
        CreateMap<CreateLabOrderRequestDto, LabOrder>();
        CreateMap<LabOrderItem, LabOrderItemResponseDto>();
        CreateMap<LabOrderItemRequestDto, LabOrderItem>();
        CreateMap<LabProfile, LabProfileResponseDto>()
            .ForMember(dest => dest.LicenseNumber,
                opt => opt.MapFrom(src => src.RegistrationNumber))
            .ForMember(dest => dest.IsHomeCollectionAvailable,
                opt => opt.Ignore()); // Entity তে নেই — default false থাকবে
        CreateMap<LabTest, LabTestResponseDto>()
            .ForMember(dest => dest.SampleType,
                opt => opt.MapFrom(src => src.SampleType.ToString()))
            .ForMember(dest => dest.TurnAroundHours,
                opt => opt.MapFrom(src => src.TurnAroundTimeHours));


        // ════════════════════════════════════════════════════════════════
        // PATIENT, NOTIFICATION, PAYMENT
        // ════════════════════════════════════════════════════════════════
        CreateMap<UserNotification, UserNotificationResponseDto>();
        CreateMap<SendNotificationRequestDto, UserNotification>();
        CreateMap<NotificationTemplate, NotificationTemplateResponseDto>();
        CreateMap<Patient, PatientSummaryResponseDto>();
        CreateMap<Patient, PatientDetailResponseDto>();
        CreateMap<CreatePatientRequestDto, Patient>();

        CreateMap<UpdatePatientRequestDto, Patient>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember is not null));

        CreateMap<PatientVital, PatientVitalResponseDto>();
        CreateMap<PatientVitalRequestDto, PatientVital>();
        CreateMap<MedicalRecord, MedicalRecordResponseDto>();
        CreateMap<PaymentTransaction, PaymentTransactionResponseDto>();
        CreateMap<CreatePaymentRequestDto, PaymentTransaction>();
        CreateMap<PlatformWallet, PlatformWalletResponseDto>();


        // ════════════════════════════════════════════════════════════════
        // PHARMACY, TELEMEDICINE, WARDS
        // ════════════════════════════════════════════════════════════════
        CreateMap<Medicine, MedicineResponseDto>();
        CreateMap<CreateMedicineRequestDto, Medicine>();
        CreateMap<PharmacyProfile, PharmacyProfileResponseDto>();
        CreateMap<Prescription, PrescriptionResponseDto>();
        CreateMap<CreatePrescriptionRequestDto, Prescription>();
        CreateMap<PrescriptionItem, PrescriptionItemResponseDto>();
        CreateMap<PrescriptionItemRequestDto, PrescriptionItem>();
        CreateMap<MedicineOrder, MedicineOrderResponseDto>();
        CreateMap<MedicineOrderRequestDto, MedicineOrder>();
        CreateMap<MedicineOrderItemRequestDto, MedicineOrderItem>();
        CreateMap<TelemedicineSession, TelemedicineSessionResponseDto>();
        CreateMap<CreateTelemedicineSessionRequestDto, TelemedicineSession>();
        CreateMap<EndSessionRequestDto, TelemedicineSession>();
        CreateMap<Admission, AdmissionResponseDto>();
        CreateMap<CreateAdmissionRequestDto, Admission>();
        CreateMap<DischargePatientRequestDto, Admission>();
        CreateMap<Bed, BedResponseDto>();
        CreateMap<Ward, WardResponseDto>();
    }
}

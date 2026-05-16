using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

using HealthcareHospitalManagement.Application.DTOs.Patient;
using HealthcareHospitalManagement.Application.DTOs.Doctor;
using HealthcareHospitalManagement.Application.DTOs.Emergency;
using HealthcareHospitalManagement.Application.DTOs.Feed;
using HealthcareHospitalManagement.Application.DTOs.Feedback;
using HealthcareHospitalManagement.Application.DTOs.Lab;
using HealthcareHospitalManagement.Application.DTOs.Notification;
using HealthcareHospitalManagement.Application.DTOs.Payment;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Application.DTOs.Telemedicine;
using HealthcareHospitalManagement.Application.Validators.Ambulance;
using HealthcareHospitalManagement.Application.Validators.Analytics;
using HealthcareHospitalManagement.Application.Validators.Appointment;
using HealthcareHospitalManagement.Application.Validators.Auth;
using HealthcareHospitalManagement.Application.Validators.Billing;
using HealthcareHospitalManagement.Application.Validators.Chat;
using HealthcareHospitalManagement.Application.Validators.Doctor;
using HealthcareHospitalManagement.Application.Validators.Emergency;
using HealthcareHospitalManagement.Application.Validators.Feed;
using HealthcareHospitalManagement.Application.Validators.Feedback;
using HealthcareHospitalManagement.Application.Validators.Identity;
using HealthcareHospitalManagement.Application.Validators.Lab;
using HealthcareHospitalManagement.Application.Validators.Notification;
using HealthcareHospitalManagement.Application.Validators.Patient;
using HealthcareHospitalManagement.Application.Validators.Payment;
using HealthcareHospitalManagement.Application.Validators.Pharmacy;
using HealthcareHospitalManagement.Application.Validators.Telemedicine;
using HealthcareHospitalManagement.Application.Validators.Wards;
using HealthcareHospitalManagement.Application.DTOs.Wards;
using HealthcareHospitalManagement.Application.DTOs.Identity;
using HealthcareHospitalManagement.Application.DTOs.Chat;
using HealthcareHospitalManagement.Application.DTOs.Billing;
using HealthcareHospitalManagement.Application.DTOs.Auth;
using HealthcareHospitalManagement.Application.DTOs.Appointment;
using HealthcareHospitalManagement.Application.DTOs.Ambulance;
using HealthcareHospitalManagement.Application.DTOs.Analytics;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;
using HealthcareHospitalManagement.Application.Validators.Feed.Posts;

namespace HealthcareHospitalManagement.Application.Helpers.ValidationsService;

public static class ValidatorExtensions
{
    public static IServiceCollection AddApplicationValidators(this IServiceCollection services)
    {
        // ── Ambulance ──────────────────────────────────────────────────────────
        services.AddTransient<IValidator<AmbulanceSearchRequestDto>, AmbulanceSearchRequestValidator>();
        services.AddTransient<IValidator<BookAmbulanceRequestDto>, BookAmbulanceRequestValidator>();
        services.AddTransient<IValidator<UpdateAmbulanceBookingStatusRequestDto>, UpdateAmbulanceBookingStatusRequestValidator>();

        // ── Analytics ─────────────────────────────────────────────────────────
        services.AddTransient<IValidator<ReportFilterRequestDto>, ReportFilterRequestValidator>();

        // ── Appointment ───────────────────────────────────────────────────────
        services.AddTransient<IValidator<AppointmentFilterRequestDto>, AppointmentFilterRequestValidator>();
        services.AddTransient<IValidator<BookAppointmentRequestDto>, BookAppointmentRequestValidator>();
        services.AddTransient<IValidator<CancelAppointmentRequestDto>, CancelAppointmentRequestValidator>();
        services.AddTransient<IValidator<RescheduleAppointmentRequestDto>, RescheduleAppointmentRequestValidator>();

        // ── Auth ──────────────────────────────────────────────────────────────
        services.AddTransient<IValidator<ChangePasswordRequestDto>, ChangePasswordRequestValidator>();
        services.AddTransient<IValidator<ForgotPasswordRequestDto>, ForgotPasswordRequestValidator>();
        services.AddTransient<IValidator<LoginRequestDto>, LoginRequestValidator>();
        services.AddTransient<IValidator<RefreshTokenRequestDto>, RefreshTokenRequestValidator>();
        services.AddTransient<IValidator<RegisterRequestDto>, RegisterRequestValidator>();
        services.AddTransient<IValidator<ResetPasswordRequestDto>, ResetPasswordRequestValidator>();

        // ── Billing ───────────────────────────────────────────────────────────
        services.AddTransient<IValidator<BillFilterRequestDto>, BillFilterRequestValidator>();
        services.AddTransient<IValidator<BillItemRequestDto>, BillItemRequestValidator>();
        services.AddTransient<IValidator<CreateBillRequestDto>, CreateBillRequestValidator>();
        services.AddTransient<IValidator<UpdatePaymentStatusRequestDto>, UpdatePaymentStatusRequestValidator>();

        // ── Chat ──────────────────────────────────────────────────────────────
        services.AddTransient<IValidator<MessageFilterRequestDto>, MessageFilterRequestValidator>();
        services.AddTransient<IValidator<SendMessageRequestDto>, SendMessageRequestValidator>();
        services.AddTransient<IValidator<StartConversationRequestDto>, StartConversationRequestValidator>();

        // ── Doctor ────────────────────────────────────────────────────────────
        services.AddTransient<IValidator<CreateDoctorRequestDto>, CreateDoctorRequestValidator>();
        services.AddTransient<IValidator<DoctorLeaveRequestDto>, DoctorLeaveRequestValidator>();
        services.AddTransient<IValidator<DoctorScheduleRequestDto>, DoctorScheduleRequestValidator>();
        services.AddTransient<IValidator<DoctorSearchRequestDto>, DoctorSearchRequestValidator>();
        services.AddTransient<IValidator<UpdateDoctorRequestDto>, UpdateDoctorRequestValidator>();

        // ── Emergency ─────────────────────────────────────────────────────────
        services.AddTransient<IValidator<CreateEmergencyVisitRequestDto>, CreateEmergencyVisitRequestValidator>();
        services.AddTransient<IValidator<EmergencyVisitFilterRequestDto>, EmergencyVisitFilterRequestValidator>();
        services.AddTransient<IValidator<UpdateEmergencyVisitRequestDto>, UpdateEmergencyVisitRequestValidator>();

        // ── Feed ──────────────────────────────────────────────────────────────
        services.AddTransient<IValidator<AddPostCommentRequestDto>, AddPostCommentRequestValidator>();
        services.AddTransient<IValidator<CreateFeedItemRequestDto>, CreateFeedItemRequestValidator>();
        services.AddTransient<IValidator<CreatePostRequestDto>, CreatePostRequestValidator>();
        services.AddTransient<IValidator<FeedFilterRequestDto>, FeedFilterRequestValidator>();

        // ── Feedback ──────────────────────────────────────────────────────────
        services.AddTransient<IValidator<CreateFeedbackRequestDto>, CreateFeedbackRequestValidator>();
        services.AddTransient<IValidator<FeedbackFilterRequestDto>, FeedbackFilterRequestValidator>();

        // ── Identity ──────────────────────────────────────────────────────────
        services.AddTransient<IValidator<AssignRoleRequestDto>, AssignRoleRequestValidator>();
        services.AddTransient<IValidator<AuditLogFilterRequestDto>, AuditLogFilterRequestValidator>();
        services.AddTransient<IValidator<VerifyOtpRequestDto>, VerifyOtpRequestValidator>();

        // ── Lab ───────────────────────────────────────────────────────────────
        services.AddTransient<IValidator<CreateLabOrderRequestDto>, CreateLabOrderRequestValidator>();
        services.AddTransient<IValidator<LabOrderFilterRequestDto>, LabOrderFilterRequestValidator>();
        services.AddTransient<IValidator<LabOrderItemRequestDto>, LabOrderItemRequestValidator>();

        // ── Notification ──────────────────────────────────────────────────────
        services.AddTransient<IValidator<MarkNotificationReadRequestDto>, MarkNotificationReadRequestValidator>();
        services.AddTransient<IValidator<NotificationFilterRequestDto>, NotificationFilterRequestValidator>();
        services.AddTransient<IValidator<SendNotificationRequestDto>, SendNotificationRequestValidator>();

        // ── Patient ───────────────────────────────────────────────────────────
        services.AddTransient<IValidator<CreatePatientRequestDto>, CreatePatientRequestValidator>();
        services.AddTransient<IValidator<PatientSearchRequestDto>, PatientSearchRequestValidator>();
        services.AddTransient<IValidator<PatientVitalRequestDto>, PatientVitalRequestValidator>();
        services.AddTransient<IValidator<UpdatePatientRequestDto>, UpdatePatientRequestValidator>();

        // ── Payment ───────────────────────────────────────────────────────────
        services.AddTransient<IValidator<CreatePaymentRequestDto>, CreatePaymentRequestValidator>();
        services.AddTransient<IValidator<RefundRequestDto>, RefundRequestValidator>();

        // ── Pharmacy ──────────────────────────────────────────────────────────
        services.AddTransient<IValidator<CreateMedicineRequestDto>, CreateMedicineRequestValidator>();
        services.AddTransient<IValidator<CreatePrescriptionRequestDto>, CreatePrescriptionRequestValidator>();
        services.AddTransient<IValidator<MedicineOrderItemRequestDto>, MedicineOrderItemRequestValidator>();
        services.AddTransient<IValidator<MedicineOrderRequestDto>, MedicineOrderRequestValidator>();
        services.AddTransient<IValidator<MedicineSearchRequestDto>, MedicineSearchRequestValidator>();
        services.AddTransient<IValidator<PrescriptionItemRequestDto>, PrescriptionItemRequestValidator>();

        // ── Telemedicine ──────────────────────────────────────────────────────
        services.AddTransient<IValidator<CreateTelemedicineSessionRequestDto>, CreateTelemedicineSessionRequestValidator>();
        services.AddTransient<IValidator<EndSessionRequestDto>, EndSessionRequestValidator>();

        // ── Wards ─────────────────────────────────────────────────────────────
        services.AddTransient<IValidator<BedFilterRequestDto>, BedFilterRequestValidator>();
        services.AddTransient<IValidator<CreateAdmissionRequestDto>, CreateAdmissionRequestValidator>();
        services.AddTransient<IValidator<DischargePatientRequestDto>, DischargePatientRequestValidator>();

        return services;
    }
}

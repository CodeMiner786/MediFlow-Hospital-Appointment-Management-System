namespace HealthcareHospitalManagement.Domain.Enums.Audit;

public enum AuditAction
{
    Login, Logout, Create, Update, Delete,
    Approve, Reject, Export, Payment, Booking,
    RoleAssign, AccountCreate, AccountSuspend, ReportGenerate,

    ForgotPassword, ResetPassword, ChangePassword

}
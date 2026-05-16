using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Notification;
using HealthcareHospitalManagement.Domain.Enums.Account;
using HealthcareHospitalManagement.Domain.Enums.LoginWith;
using HealthcareHospitalManagement.Domain.Enums.UserRole;

namespace HealthcareHospitalManagement.Domain.Entities.Identity
{
    public class ApplicationUser : BaseEntity
    {
        // ── Identity ─────────────────────────────────────────────────
        public      string              FirstName               { get; set; } = string.Empty;
        public      string              LastName                { get; set; } = string.Empty;
        public      string              FullName                => $"{FirstName} {LastName}";
        public      string?             ProfileImageUrl         { get; set; }
        public      string              PreferredLanguage       { get; set; } = "en";

        // ── Credentials ───────────────────────────────────────────────
        public      string              Email                   { get; set; } = string.Empty;
        public      string              NormalizedEmail         { get; set; } = string.Empty;
        public      string?             PhoneNumber             { get; set; }
        public      string              PasswordHash            { get; set; } = string.Empty;
        public      string?             PasswordSalt            { get; set; }

        // ── Role & Status ─────────────────────────────────────────────
        public      UserRole            Role                    { get; set; } = UserRole.User;
        public      AccountStatus       AccountStatus           { get; set; } = AccountStatus.Pending;
        public      Guid?               LinkedProfileId         { get; set; } // Role-specific profile link

        // ── Verification ──────────────────────────────────────────────
        public      bool                IsEmailVerified         { get; set; } = false;
        public      DateTime?           EmailVerifiedAt         { get; set; }
        public      bool                IsPhoneVerified         { get; set; } = false;
        public      DateTime?           PhoneVerifiedAt         { get; set; }

        // ── 2FA ───────────────────────────────────────────────────────
        public      bool                IsTwoFactorEnabled      { get; set; } = false;
        public      string?             TwoFactorSecretKey      { get; set; }

        // ── Login Tracking ────────────────────────────────────────────
        public      DateTime?           LastLoginAt             { get; set; }
        public      string?             LastLoginIp             { get; set; }
        public      int                 FailedLoginAttempts     { get; set; } = 0;
        public      DateTime?           LockoutEndAt            { get; set; }
        public      bool                IsLockedOut             => LockoutEndAt.HasValue && LockoutEndAt > DateTime.UtcNow;

        // ── External Auth ─────────────────────────────────────────────
        public      LoginProvider       LoginProvider           { get; set; } = LoginProvider.Local;
        public      string?             ExternalProviderId      { get; set; }

        // ── Tracking ──────────────────────────────────────────────────
        public      Guid?               CreatedByAdminId        { get; set; }

        // ── Navigation Collections ────────────────────────────────────
        public      ICollection<OtpCode>                OtpCodes            { get; set; } = [];
        public      ICollection<UserLoginHistory>       LoginHistories      { get; set; } = [];
        public      ICollection<UserNotification>       Notifications       { get; set; } = [];
        public      ICollection<UserPermissionOverride> PermissionOverrides { get; set; } = [];
        public      ICollection<RoleAssignmentLog>      RoleAssignmentLogs  { get; set; } = [];
        public      ICollection<UserRefreshToken>       UserRefreshTokens   { get; set; } = [];
    }
}
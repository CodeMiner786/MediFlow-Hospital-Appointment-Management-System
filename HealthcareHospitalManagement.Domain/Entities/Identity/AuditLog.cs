using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Audit;

namespace HealthcareHospitalManagement.Domain.Entities.Identity
{
    /// <summary>
    /// Full audit trail for SuperAdmin reporting.
    /// SuperAdmin can export this as PDF/Word/Excel from dashboard.
    /// </summary>
    public class AuditLog : BaseEntity
    {
        // ——— ১. ইউজার ইনফরমেশন ———
        public      Guid?               UserId                  { get; set; }
        public      string?             UserEmail               { get; set; }
        public      string?             UserRole                { get; set; }

        // ——— ২. অ্যাকশন ও ডাটা চেঞ্জেস ———
        public      AuditAction         Action                  { get; set; }
        public      string              EntityName              { get; set; } = string.Empty;
        public      string?             EntityId                { get; set; }
        public      string?             OldValues               { get; set; } // JSON format
        public      string?             NewValues               { get; set; } // JSON format
        public      string?             Description             { get; set; }

        // ——— ৩. মেটা ডাটা ও স্ট্যাটাস ———
        public      string?             IpAddress               { get; set; }
        public      string?             UserAgent               { get; set; }
        public      bool                IsSuccess               { get; set; } = true;
        public      string?             ErrorMessage            { get; set; }
        public      DateTime            Timestamp               { get; set; } = DateTime.UtcNow;
    }
}
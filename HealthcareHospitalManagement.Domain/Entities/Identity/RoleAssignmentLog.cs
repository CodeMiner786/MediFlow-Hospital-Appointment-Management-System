using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.UserRole;

namespace HealthcareHospitalManagement.Domain.Entities.Identity
{
    public class RoleAssignmentLog : BaseEntity
    {
        // ——— ১. টার্গেট ইউজার (যাঁর রোল চেঞ্জ হচ্ছে) ———
        public      Guid                UserId                  { get; set; }

        public      ApplicationUser     User                    { get; set; } = null!;


        // ——— ২. রোলের পরিবর্তন ———
        public      UserRole            FromRole                { get; set; }

        public      UserRole            ToRole                  { get; set; }


        // ——— ৩. কে পরিবর্তন করেছেন (Admin/SuperAdmin) ———
        public      Guid                AssignedByUserId        { get; set; }

        public      ApplicationUser     AssignedByUser          { get; set; } = null!;

        public      string              AssignedByEmail         { get; set; } = string.Empty;

        public      UserRole            AssignedByRole          { get; set; }


        // ——— ৪. মেটা ডাটা ও ট্র্যাকিং ———
        public      DateTime            AssignedAt              { get; set; } = DateTime.UtcNow;

        public      string?             Reason                  { get; set; }

        public      string?             IpAddress               { get; set; }

        public      string?             UserAgent               { get; set; }
    }
}
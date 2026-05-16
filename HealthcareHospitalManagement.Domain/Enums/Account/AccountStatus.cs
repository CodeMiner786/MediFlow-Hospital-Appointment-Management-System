namespace HealthcareHospitalManagement.Domain.Enums.Account
{
    public enum AccountStatus
    {
        Pending = 1,   // Register করেছে, OTP verify করেনি
        Active = 2,   // OTP verify হয়েছে
        Suspended = 3,   // Admin suspend করেছে
        Banned = 4    // Admin ban করেছে
    }
}

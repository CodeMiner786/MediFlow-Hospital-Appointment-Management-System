namespace HealthcareHospitalManagement.Domain.Enums.UserRole
{
    public enum UserRole
    {
        User = 1,   // Default — সবাই এটা দিয়ে শুরু করে
        Doctor = 2,   // Admin assign করবে
        Lab = 3,   // Admin assign করবে
        Ambulance = 4,   // Admin assign করবে
        Pharmacy = 5,   // Admin assign করবে
        Admin = 6,   // SuperAdmin assign করবে
        SuperAdmin = 7    // System-এ hardcode থাকবে
    }
}

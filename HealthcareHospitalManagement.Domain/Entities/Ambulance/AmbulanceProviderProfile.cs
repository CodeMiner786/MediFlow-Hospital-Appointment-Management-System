using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Identity;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;

namespace HealthcareHospitalManagement.Domain.Entities.Ambulance;

    public class AmbulanceProviderProfile : BaseEntity
    {
        // ── Identity & Account ───────────────────────────────────────────────────────────────────────
        public    Guid                          ApplicationUserId   { get; set; }
        public    ApplicationUser               ApplicationUser     { get; set; } = null!;

        // ── Provider Details ─────────────────────────────────────────────────────────────────────────
        public    string                        ProviderName        { get; set; } = string.Empty;
        public    string                        RegistrationNumber  { get; set; } = string.Empty;
        public    string                        ContactNumber       { get; set; } = string.Empty;
        public    string                        EmergencyHotline    { get; set; } = string.Empty;
        public    string?                       LogoUrl             { get; set; }
        public    string?                       Website             { get; set; }

        // ── Location Details ─────────────────────────────────────────────────────────────────────────
        public    string                        Address             { get; set; } = string.Empty;
        public    string                        City                { get; set; } = string.Empty;
        public    string                        District            { get; set; } = string.Empty;
        public    double                        Latitude            { get; set; }
        public    double                        Longitude           { get; set; }

        // ── Status & Verification ────────────────────────────────────────────────────────────────────
        public    bool                          IsVerified          { get; set; } = false;
        public    DateTime?                     VerifiedAt          { get; set; }
        public    AmbulanceProviderStatus       Status              { get; set; } = AmbulanceProviderStatus.PendingVerification;

        // ── Ratings & Feedback ───────────────────────────────────────────────────────────────────────
        public    decimal                       AverageRating       { get; set; } = 0;
        public    int                           TotalRatings        { get; set; } = 0;

        // ── Navigation Properties ────────────────────────────────────────────────────────────────────
        public    ICollection<AmbulanceVehicle>         Vehicles        { get; set; } = [];
        public    ICollection<AmbulanceBooking>         Bookings        { get; set; } = [];
        public    ICollection<AmbulanceServiceListing>  ServiceListings { get; set; } = [];
        public    AmbulanceProviderWallet?              Wallet          { get; set; }
    }
using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using HealthcareHospitalManagement.Domain.Enums.Feed;

namespace HealthcareHospitalManagement.Domain.Entities.Ambulance
{
    public class AmbulanceServiceListing : BaseEntity
    {
        // ── Foreign Keys & Navigation ──────────────────────────────────────────────────────────────────
        public    Guid                      ProviderId              { get; set; }
        public    AmbulanceProviderProfile  Provider                { get; set; } = null!;

        // ── Service Details ────────────────────────────────────────────────────────────────────────────
        public    string                    ServiceTitle            { get; set; } = string.Empty;
        public    string?                   Description             { get; set; }
        public    AmbulanceCategory         Category                { get; set; }
        public    string?                   ImageUrl                { get; set; }

        // ── Pricing & Status ───────────────────────────────────────────────────────────────────────────
        public    decimal                   BasePrice               { get; set; }
        public    bool                      IsAvailable             { get; set; } = true;
        public    FeedItemStatus            FeedStatus              { get; set; } = FeedItemStatus.Active;
    }
}
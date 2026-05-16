using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;

namespace HealthcareHospitalManagement.Domain.Entities.Ambulance
{
    public class AmbulanceVehicle : BaseEntity
    {
        // ── Foreign Keys & Navigation ──────────────────────────────────────────────────────────────────
        public    Guid                      ProviderId               { get; set; }
        public    AmbulanceProviderProfile  Provider                 { get; set; } = null!;

        // ── Vehicle Identification ─────────────────────────────────────────────────────────────────────
        public    string                    VehicleNumber            { get; set; } = string.Empty;
        public    string                    AmbulanceCode            { get; set; } = string.Empty;
        public    AmbulanceCategory         Category                 { get; set; }
        public    AmbulanceStatus           Status                   { get; set; } = AmbulanceStatus.Available;

        // ── Medical Equipment ──────────────────────────────────────────────────────────────────────────
        public    bool                      HasVentilator            { get; set; }
        public    bool                      HasDefibrillator         { get; set; }
        public    bool                      HasOxygen                { get; set; }
        public    bool                      HasStretcherWheelchair   { get; set; }

        // ── GPS Tracking ───────────────────────────────────────────────────────────────────────────────
        public    double?                   CurrentLatitude          { get; set; }
        public    double?                   CurrentLongitude         { get; set; }
        public    DateTime?                 LastLocationUpdatedAt    { get; set; }

        // ── Staff Details ──────────────────────────────────────────────────────────────────────────────
        public    string                    DriverName               { get; set; } = string.Empty;
        public    string                    DriverPhone              { get; set; } = string.Empty;
        public    string?                   ParamedicName            { get; set; }
        public    string?                   ParamedicPhone           { get; set; }

        // ── Maintenance & Logs ─────────────────────────────────────────────────────────────────────────
        public    DateTime                  LastMaintenanceDate      { get; set; }
        public    DateTime                  NextMaintenanceDue       { get; set; }
        public    string?                   Notes                    { get; set; }

        // ── Navigation Properties ──────────────────────────────────────────────────────────────────────
        public    ICollection<AmbulanceBooking> Bookings             { get; set; } = [];
    }
}
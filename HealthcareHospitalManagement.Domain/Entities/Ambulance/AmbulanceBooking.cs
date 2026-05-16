using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Entities.Patients;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;

namespace HealthcareHospitalManagement.Domain.Entities.Ambulance
{
    public class AmbulanceBooking : BaseEntity
    {
        // ── Identification ─────────────────────────────────────────────────────────────────────────────
        public    string                    BookingCode             { get; set; } = string.Empty;

        // ── Foreign Keys & Navigation ──────────────────────────────────────────────────────────────────
        public    Guid                      VehicleId               { get; set; }
        public    AmbulanceVehicle          Vehicle                 { get; set; } = null!;

        public    Guid                      ProviderId              { get; set; }
        public    AmbulanceProviderProfile  Provider                { get; set; } = null!;

        public    Guid?                     PatientId               { get; set; }
        public    Patient?                  Patient                 { get; set; }

        // ── Requester Info ─────────────────────────────────────────────────────────────────────────────
        public    string                    RequesterName           { get; set; } = string.Empty;
        public    string                    RequesterPhone          { get; set; } = string.Empty;

        // ── Status & Timestamps ────────────────────────────────────────────────────────────────────────
        public    AmbulanceBookingStatus    Status                  { get; set; } = AmbulanceBookingStatus.Requested;
        public    DateTime                  RequestedAt             { get; set; } = DateTime.UtcNow;
        public    DateTime?                 DispatchedAt            { get; set; }
        public    DateTime?                 ArrivedAt               { get; set; }
        public    DateTime?                 CompletedAt             { get; set; }

        // ── Location Details ───────────────────────────────────────────────────────────────────────────
        public    string                    PickupAddress           { get; set; } = string.Empty;
        public    double?                   PickupLatitude          { get; set; }
        public    double?                   PickupLongitude         { get; set; }

        public    string?                   DropAddress             { get; set; }
        public    double?                   DropLatitude            { get; set; }
        public    double?                   DropLongitude           { get; set; }

        // ── Additional Info ────────────────────────────────────────────────────────────────────────────
        public    string?                   EmergencyDescription    { get; set; }
        public    string?                   CancellationReason      { get; set; }
        public    string?                   Notes                   { get; set; }

        // ── Financial Info ─────────────────────────────────────────────────────────────────────────────
        public    decimal?                  Fare                    { get; set; }
        public    bool                      IsPaid                  { get; set; } = false;
        public    PaymentDestination        PaymentDestination      { get; set; } = PaymentDestination.AmbulanceProviderWallet;
    }
}
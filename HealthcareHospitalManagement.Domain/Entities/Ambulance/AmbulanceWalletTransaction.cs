using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Ambulance;

namespace HealthcareHospitalManagement.Domain.Entities.Ambulance
{
    public class AmbulanceWalletTransaction : BaseEntity
    {
        // ── Foreign Keys & Navigation ──────────────────────────────────────────────────────────────────
        public    Guid                      WalletId                { get; set; }
        public    AmbulanceProviderWallet   Wallet                  { get; set; } = null!;

        public    Guid?                     BookingId               { get; set; }
        public    AmbulanceBooking?         Booking                 { get; set; }

        // ── Transaction Details ────────────────────────────────────────────────────────────────────────
        public    string                    TransactionCode         { get; set; } = string.Empty;
        public    decimal                   Amount                  { get; set; }
        public    WalletTransactionType     Type                    { get; set; }
        public    string?                   Description             { get; set; }
        public    DateTime                  TransactionAt           { get; set; } = DateTime.UtcNow;

        // ── Balance Audit ──────────────────────────────────────────────────────────────────────────────
        public    decimal                   BalanceAfter            { get; set; }
    }
}
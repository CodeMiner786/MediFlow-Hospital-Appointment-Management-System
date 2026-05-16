using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Ambulance
{
    public class AmbulanceProviderWallet : BaseEntity
    {
        // ── Foreign Keys & Navigation ──────────────────────────────────────────────────────────────────
        public    Guid                          ProviderId              { get; set; }
        public    AmbulanceProviderProfile      Provider                { get; set; } = null!;

        // ── Balance & Financial Info ───────────────────────────────────────────────────────────────────
        public    decimal                       Balance                 { get; set; } = 0;
        public    decimal                       TotalEarned             { get; set; } = 0;
        public    decimal                       TotalWithdrawn          { get; set; } = 0;
        public    DateTime?                     LastTransactionAt       { get; set; }

        // ── Navigation Properties ──────────────────────────────────────────────────────────────────────
        public    ICollection<AmbulanceWalletTransaction>  Transactions { get; set; } = [];
    }
}
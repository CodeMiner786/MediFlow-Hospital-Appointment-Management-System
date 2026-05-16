using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Payment
{
    /// <summary>
    /// Platform-level wallet owned by SuperAdmin.
    /// Receives all payments EXCEPT ambulance booking payments.
    /// </summary>
    public class PlatformWallet : BaseEntity
    {
        // ——— ১. ব্যালেন্স সামারি ———
        public      decimal             TotalBalance            { get; set; } = 0;

        public      decimal             TotalReceived           { get; set; } = 0;

        public      decimal             TotalRefunded           { get; set; } = 0;


        // ——— ২. অ্যাক্টিভিটি ট্র্যাকিং ———
        public      DateTime?           LastTransactionAt       { get; set; }


        // ——— ৩. নেভিগেশন ———
        public      ICollection<PlatformWalletTransaction> Transactions { get; set; } = [];
    }
}
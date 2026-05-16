using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Payment
{
    /// <summary>
    /// Each credit/debit entry in the PlatformWallet.
    /// TransactionType: "DoctorPayment" | "LabPayment" | "PharmacyPayment" | "BedBooking" | "Refund"
    /// </summary>
    public class PlatformWalletTransaction : BaseEntity
    {
        // ——— ১. ওয়ালেট রেফারেন্স ———
        public      Guid                WalletId                { get; set; }
        public      PlatformWallet      Wallet                  { get; set; } = null!;


        // ——— ২. পেমেন্ট সোর্স ———
        public      Guid?               PaymentId               { get; set; }
        public      PaymentTransaction? Payment                 { get; set; } // আপনার আগের এনটিটি অনুযায়ী PaymentTransaction হবে


        // ——— ৩. ট্রানজ্যাকশন ডিটেইলস ———
        public      string              TransactionCode         { get; set; } = string.Empty;

        public      decimal             Amount                  { get; set; }

        public      string              TransactionType         { get; set; } = string.Empty;

        public      string?             Description             { get; set; }

        public      DateTime            TransactionAt           { get; set; } = DateTime.UtcNow;


        // ——— ৪. অডিট ট্রেইল ———
        public      decimal             BalanceAfter            { get; set; } // ট্রানজ্যাকশন পরবর্তী ব্যালেন্স
    }
}
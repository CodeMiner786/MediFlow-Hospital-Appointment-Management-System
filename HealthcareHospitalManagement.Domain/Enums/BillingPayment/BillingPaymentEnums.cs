namespace HealthcareHospitalManagement.Domain.Enums.BillingPayment
{
    public enum PaymentStatus        { Pending, Paid, PartiallyPaid, Refunded, Cancelled, Overdue,
        Successful
    }
    public enum PaymentMethod        { Cash, Card, BankTransfer, Insurance, Bkash, Nagad, Rocket, Stripe, PayPal, Cheque }
    public enum PaymentGateway       { Bkash, Nagad, Rocket, Stripe, PayPal, SSLCommerz, Manual }
    public enum BillType             { OPD, IPD, Emergency, Lab, Pharmacy, Surgery, Consultation, Ambulance, Telemedicine, BedBooking }
    public enum InsuranceClaimStatus { Submitted, UnderReview, Approved, Rejected, Settled }
    public enum InvoiceStatus        { Draft, Issued, Paid, Overdue, Cancelled }

    /// <summary>
    /// Determines where payment amount is routed.
    /// AmbulanceBooking → AmbulanceProvider wallet
    /// All others       → SuperAdmin/Platform wallet
    /// </summary>
    public enum PaymentDestination   { PlatformWallet, AmbulanceProviderWallet }
}

namespace HealthcareHospitalManagement.Domain.Enums.Notification
{
    public enum NotificationChannel  { InApp, Email, SMS, Push }
    public enum NotificationStatus   { Pending, Sent, Failed, Read }
    public enum NotificationCategory { Appointment, Booking, Payment, Lab, Ambulance, Medicine, System, Alert, Referral, Chat, BedBooking,
        Security
    }
}

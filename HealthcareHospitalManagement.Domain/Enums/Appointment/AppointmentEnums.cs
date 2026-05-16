namespace HealthcareHospitalManagement.Domain.Enums.Appointment
{
    public enum AppointmentStatus { Scheduled, Confirmed, Completed, Cancelled, NoShow, Rescheduled }
    public enum AppointmentType   { InPerson, Telemedicine, FollowUp, Emergency }
}

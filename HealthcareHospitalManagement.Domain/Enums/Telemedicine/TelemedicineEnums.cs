namespace HealthcareHospitalManagement.Domain.Enums.Telemedicine
{
    public enum TelemedicineSessionStatus { Scheduled, Waiting, InProgress, Completed, Missed, Cancelled , Live}
    public enum VideoCallProvider         { Jitsi, Twilio, Zoom, Agora }
}

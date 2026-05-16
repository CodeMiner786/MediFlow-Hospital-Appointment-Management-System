namespace HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance
{
    public enum EmergencyLevel         { Green, Yellow, Orange, Red, Black }
    public enum EmergencyStatus        { Arrived, Triaged, UnderTreatment, Stabilized, Admitted, Transferred, Discharged, Deceased }
    public enum AmbulanceStatus        { Available, Dispatched, EnRoute, AtScene, TransportingPatient, AtHospital, UnderMaintenance, OnTrip }
    public enum AmbulanceCategory      { General, ICU, VIP, Neonatal, AirAmbulance }
    public enum AmbulanceBookingStatus { Requested, Confirmed, Dispatched, Completed, Cancelled, Arrived }
    public enum AmbulanceProviderStatus{ PendingVerification, Active, Suspended, Inactive }
}

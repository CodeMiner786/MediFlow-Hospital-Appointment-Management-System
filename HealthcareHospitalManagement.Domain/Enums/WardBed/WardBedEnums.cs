namespace HealthcareHospitalManagement.Domain.Enums.WardBed
{
    public enum WardType       { General, ICU, Emergency, Maternity, Pediatric, Surgical, Orthopedic, Cardiac, Psychiatric, Oncology }
    public enum BedStatus      { Available, Occupied, UnderMaintenance, Reserved }
    public enum AdmissionStatus{ Admitted, Discharged, Transferred, UnderObservation }
}

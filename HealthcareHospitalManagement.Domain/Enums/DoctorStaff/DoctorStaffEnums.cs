namespace HealthcareHospitalManagement.Domain.Enums.DoctorStaff
{
    public enum StaffType                { Doctor, Nurse, Technician, Receptionist, Admin, Pharmacist, LabTechnician }
    public enum DoctorAvailabilityStatus { Available, OnLeave, OffDuty, InSurgery, Online, Offline }
    public enum ShiftType                { Morning, Evening, Night, Rotation }
}

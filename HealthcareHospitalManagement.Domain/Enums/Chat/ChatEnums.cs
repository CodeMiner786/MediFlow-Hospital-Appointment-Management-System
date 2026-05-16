namespace HealthcareHospitalManagement.Domain.Enums.Chat
{
    /// <summary>
    /// Users can chat with Doctor, AmbulanceProvider, Lab, Pharmacy separately.
    /// </summary>
    public enum ChatParticipantType { User, Doctor, Nurse, AmbulanceProvider, LabAdmin, PharmacyAdmin, SuperAdmin }
    public enum MessageStatus       { Sent, Delivered, Read, Deleted }
    public enum ConversationType    { UserDoctor, UserAmbulance, UserLab, UserPharmacy, UserAdmin, General }
}

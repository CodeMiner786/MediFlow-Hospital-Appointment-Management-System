namespace HealthcareHospitalManagement.Domain.Enums.Patient
{
    public enum Gender          { Male, Female, Other }
    public enum MaritalStatus   { Single, Married, Divorced, Widowed }
    public enum PatientType     { General, VIP, Emergency, OPD, IPD }
    public enum BloodGroup
    {
        APositive, ANegative,
        BPositive, BNegative,
        ABPositive, ABNegative,
        OPositive, ONegative,
        Unknown
    }
}

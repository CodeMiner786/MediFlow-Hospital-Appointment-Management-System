namespace HealthcareHospitalManagement.Domain.Enums.Lab
{
    public enum LabTestStatus { Requested, SampleCollected, InProgress, Completed, Cancelled, ReportDelivered }
    public enum SampleType    { Blood, Urine, Stool, Sputum, Tissue, Swab, CSF }
    public enum TestPriority  { Routine, Urgent, STAT }
}

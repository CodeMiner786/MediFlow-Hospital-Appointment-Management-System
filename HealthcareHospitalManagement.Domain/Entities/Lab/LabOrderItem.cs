using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Lab;

namespace HealthcareHospitalManagement.Domain.Entities.Lab
{
    public class LabOrderItem : BaseEntity
    {
        // ——— ১. অর্ডার ও টেস্ট রেফারেন্স ———
        public      Guid                LabOrderId              { get; set; }

        public      LabOrder            LabOrder                { get; set; } = null!;

        public      Guid                LabTestId               { get; set; }

        public      LabTest             LabTest                 { get; set; } = null!;


        // ——— ২. স্যাম্পল কালেকশন ডিটেইলস ———
        public      LabTestStatus       Status                  { get; set; } = LabTestStatus.Requested;

        public      DateTime?           SampleCollectedAt       { get; set; }

        public      string?             SampleCollectedBy       { get; set; }

        public      string?             SampleBarcode           { get; set; }


        // ——— ৩. টেস্ট রেজাল্ট ও প্যারামিটার ———
        public      string?             ResultValue             { get; set; }

        public      string?             ResultUnit              { get; set; }

        public      string?             ReferenceRange          { get; set; }

        public      bool                IsAbnormal              { get; set; }

        public      string?             Remarks                 { get; set; }


        // ——— ৪. ট্র্যাকিং ও ডেলিভারি ———
        public      DateTime?           ResultEnteredAt         { get; set; }

        public      string?             ResultEnteredBy         { get; set; }

        public      bool                IsAutoUploaded          { get; set; } = false;

        public      DateTime?           ReportDeliveredAt       { get; set; }

        public      string?             ReportUrl               { get; set; }
    }
}
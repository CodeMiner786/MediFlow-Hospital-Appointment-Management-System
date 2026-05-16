using HealthcareHospitalManagement.Domain.Common.BaseModel;
using HealthcareHospitalManagement.Domain.Enums.Lab;

namespace HealthcareHospitalManagement.Domain.Entities.Lab
{
    public class LabTest : BaseEntity
    {
        // ——— ১. ল্যাব রেফারেন্স ———
        public      Guid                LabProfileId            { get; set; }

        public      LabProfile          LabProfile              { get; set; } = null!;


        // ——— ২. টেস্ট আইডেন্টিফিকেশন ———
        public      string              TestName                { get; set; } = string.Empty;

        public      string              TestCode                { get; set; } = string.Empty;

        public      string              Category                { get; set; } = string.Empty;

        public      string?             Description             { get; set; }


        // ——— ৩. টেকনিক্যাল ডিটেইলস ———
        public      SampleType          SampleType              { get; set; }

        public      string?             Unit                    { get; set; }

        public      string?             ReferenceRange          { get; set; }

        public      string?             Preparation             { get; set; }


        // ——— ৪. লজিস্টিকস ও কস্টিং ———
        public      decimal             Price                   { get; set; }

        public      int                 TurnAroundTimeHours     { get; set; }

        public      string?             EquipmentCode           { get; set; }

        public      bool                IsActive                { get; set; } = true;


        // ——— ৫. কালেকশনস ———
        public      ICollection<LabOrderItem>   OrderItems      { get; set; } = [];
    }
}
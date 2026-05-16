using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Patients
{
    public class PatientVital : BaseEntity
    {
        // ——— ১. পেশেন্ট রেফারেন্স ———
        public      Guid                PatientId               { get; set; }
        public      Patient             Patient                 { get; set; } = null!;


        // ——— ২. রেকর্ডিং ইনফো ———
        public      DateTime            RecordedAt              { get; set; } = DateTime.UtcNow;

        public      string              RecordedByName          { get; set; } = string.Empty;


        // ——— ৩. ভাইটাল প্যারামিটারস (Data Only) ———
        public      decimal?            TemperatureCelsius      { get; set; }

        public      int?                PulseRate               { get; set; }

        public      int?                RespiratoryRate         { get; set; }

        public      int?                BloodPressureSystolic   { get; set; }

        public      int?                BloodPressureDiastolic  { get; set; }

        public      decimal?            OxygenSaturation        { get; set; }

        public      decimal?            WeightKg                { get; set; }

        public      decimal?            HeightCm                { get; set; }

        public      decimal?            BMI                     { get; set; } // Set via Handler

        public      decimal?            BloodGlucose            { get; set; }


        // ——— ৪. অতিরিক্ত নোটস ———
        public      string?             Notes                   { get; set; }
    }
}
using HealthcareHospitalManagement.Domain.Common.BaseModel;

namespace HealthcareHospitalManagement.Domain.Entities.Patients
{
    public class HealthLog : BaseEntity
    {
        // ——— ১. পেশেন্ট রেফারেন্স ———
        public      Guid                PatientId               { get; set; }

        public      Patient             Patient                 { get; set; } = null!;


        // ——— ২. সোর্স ও টাইমিং ———
        public      DateTime            LoggedAt                { get; set; } = DateTime.UtcNow;

        public      string?             Source                  { get; set; } // Manual, Smartwatch, BPMonitor...


        // ——— ৩. ভাইটাল সাইন (Vitals) ———
        public      decimal?            WeightKg                { get; set; }

        public      decimal?            HeightCm                { get; set; }

        public      int?                BloodPressureSystolic   { get; set; }

        public      int?                BloodPressureDiastolic  { get; set; }

        public      decimal?            BloodGlucose            { get; set; }

        public      int?                HeartRate               { get; set; }

        public      decimal?            OxygenSaturation        { get; set; }

        public      decimal?            TemperatureCelsius      { get; set; }


        // ——— ৪. অ্যাক্টিভিটি ও লাইফস্টাইল ———
        public      int?                StepsCount              { get; set; }

        public      int?                SleepHours              { get; set; }

        public      string?             Notes                   { get; set; }


        // ——— ৫. ডিভাইস ডিটেইলস ———
        public      string?             DeviceId                { get; set; }

        public      string?             DeviceModel             { get; set; }
    }
}
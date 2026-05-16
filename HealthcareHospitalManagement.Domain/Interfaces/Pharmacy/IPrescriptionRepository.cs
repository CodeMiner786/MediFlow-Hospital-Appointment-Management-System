using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Enums.PharmacyMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Pharmacy
{
    public interface IPrescriptionRepository : IGenericRepository<Prescription>
    {
        // ১. প্রেসক্রিপশন কোড (Unique Code) দিয়ে খুঁজে বের করা
        Task<Prescription?> GetByCodeAsync(string prescriptionCode, CancellationToken ct = default);

        // ২. নির্দিষ্ট একজন পেশেন্টের সব প্রেসক্রিপশন হিস্টোরি দেখা
        Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(Guid patientId, CancellationToken ct = default);

        // ৩. একজন ডক্টরের দেওয়া সব প্রেসক্রিপশন দেখা
        Task<IEnumerable<Prescription>> GetPrescriptionsByDoctorIdAsync(Guid doctorId, CancellationToken ct = default);

        // ৪. স্ট্যাটাস অনুযায়ী প্রেসক্রিপশন ফিল্টার (Active, Expired, Completed)
        Task<IEnumerable<Prescription>> GetByStatusAsync(PrescriptionStatus status, CancellationToken ct = default);

        // ৫. মেয়াদোত্তীর্ণ প্রেসক্রিপশনগুলো খুঁজে বের করা
        Task<IEnumerable<Prescription>> GetExpiredPrescriptionsAsync(CancellationToken ct = default);

        // ৬. রিফিল করার যোগ্য (Refillable) প্রেসক্রিপশনগুলো দেখা
        Task<IEnumerable<Prescription>> GetRefillablePrescriptionsAsync(Guid patientId, CancellationToken ct = default);
    }
}

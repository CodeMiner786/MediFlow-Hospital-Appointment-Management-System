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
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        // ১. মেডিসিন কোড বা বারকোড দিয়ে ওষুধ খুঁজে বের করা
        Task<Medicine?> GetByCodeAsync(string medicineCode, CancellationToken ct = default);

        // ২. একই জেনেরিক নামের সব ওষুধ খুঁজে বের করা (বিকল্প ওষুধ দেখানোর জন্য)
        Task<IEnumerable<Medicine>> GetByGenericNameAsync(string genericName, CancellationToken ct = default);

        // ৩. নির্দিষ্ট ক্যাটাগরি অনুযায়ী ওষুধ ফিল্টার করা (e.g., Antibiotics, Vitamin)
        Task<IEnumerable<Medicine>> GetByCategoryAsync(MedicineCategory category, CancellationToken ct = default);

        // ৪. নাম, ব্র্যান্ড বা জেনেরিক নাম দিয়ে স্মার্ট সার্চ করা
        Task<IEnumerable<Medicine>> SearchMedicinesAsync(string searchTerm, CancellationToken ct = default);

        // ৫. প্রেসক্রিপশন লাগে এমন ওষুধগুলোর লিস্ট দেখা
        Task<IEnumerable<Medicine>> GetPrescriptionRequiredMedicinesAsync(CancellationToken ct = default);

        // ৬. নির্দিষ্ট ফার্মেসির আন্ডারে থাকা সব ওষুধ দেখা
        Task<IEnumerable<Medicine>> GetMedicinesByPharmacyIdAsync(Guid pharmacyId, CancellationToken ct = default);
    }
}

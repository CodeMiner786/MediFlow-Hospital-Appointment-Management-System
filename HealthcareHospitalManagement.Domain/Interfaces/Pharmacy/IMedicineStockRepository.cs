using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Pharmacy
{
    public interface IMedicineStockRepository : IGenericRepository<MedicineStock>
    {
        // ১. নির্দিষ্ট ওষুধের সব ব্যাচের স্টক দেখা
        Task<IEnumerable<MedicineStock>> GetStocksByMedicineIdAsync(Guid medicineId, CancellationToken ct = default);

        // ২. এক্সপায়ারি ডেট খুব কাছে এমন ওষুধগুলো খুঁজে বের করা (Alert system)
        Task<IEnumerable<MedicineStock>> GetExpiringSoonStocksAsync(int daysCount, CancellationToken ct = default);

        // ৩. স্টক কম (Low Stock) এমন ওষুধগুলো খুঁজে বের করা (Reorder Level logic)
        Task<IEnumerable<MedicineStock>> GetLowStockMedicinesAsync(CancellationToken ct = default);

        // ৪. ব্যাচ নম্বর দিয়ে নির্দিষ্ট স্টক খুঁজে বের করা
        Task<MedicineStock?> GetByBatchNumberAsync(string batchNumber, CancellationToken ct = default);

        // ৫. নির্দিষ্ট সাপ্লাইয়ারের কাছ থেকে আসা স্টকের লিস্ট
        Task<IEnumerable<MedicineStock>> GetStocksBySupplierIdAsync(Guid supplierId, CancellationToken ct = default);

        // ৬. এক্সপায়ার হয়ে গেছে এমন ওষুধের লিস্ট
        Task<IEnumerable<MedicineStock>> GetExpiredStocksAsync(CancellationToken ct = default);
    }
}

using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Billing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Billing
{
    public interface IBillItemRepository : IGenericRepository<BillItem>
    {
        // নির্দিষ্ট একটি বিলের (BillId) অধীনে থাকা সব আইটেম খুঁজে বের করা
        IAsyncEnumerable<BillItem> GetItemsByBillIdStream(Guid billId);

        // সার্ভিস কোড (ServiceCode) দিয়ে নির্দিষ্ট বিল আইটেমগুলো সার্চ করা
        Task<IEnumerable<BillItem>> GetItemsByServiceCodeAsync(string serviceCode);
    }
}

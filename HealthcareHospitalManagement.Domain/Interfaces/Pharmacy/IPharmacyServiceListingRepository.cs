using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Enums.Feed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Pharmacy
{
    public interface IPharmacyServiceListingRepository : IGenericRepository<PharmacyServiceListing>
    {
        // ১. একটি নির্দিষ্ট ফার্মেসির সব লিস্টিং দেখা
        Task<IEnumerable<PharmacyServiceListing>> GetByPharmacyIdAsync(Guid pharmacyId, CancellationToken ct = default);

        // ২. নিউজ ফিডের জন্য সব একটিভ লিস্টিং লোড করা
        Task<IEnumerable<PharmacyServiceListing>> GetActiveFeedItemsAsync(CancellationToken ct = default);

        // ৩. ডেলিভারি সার্ভিস যুক্ত লিস্টিংগুলো আলাদা করা
        Task<IEnumerable<PharmacyServiceListing>> GetDeliveryServiceListingsAsync(CancellationToken ct = default);

        // ৪. ফিড স্ট্যাটাস অনুযায়ী ফিল্টার করা (e.g., শুধু Pending বা Active)
        Task<IEnumerable<PharmacyServiceListing>> GetByFeedStatusAsync(FeedItemStatus status, CancellationToken ct = default);

        // ৫. নির্দিষ্ট টাইটেল দিয়ে সার্চ করা
        Task<IEnumerable<PharmacyServiceListing>> SearchListingsAsync(string titlePart, CancellationToken ct = default);
    }
}

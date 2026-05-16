using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.Enums.Feed;

namespace HealthcareHospitalManagement.Domain.Interfaces.Lab
{
    public interface ILabServiceListingRepository : IGenericRepository<LabServiceListing>
    {
        // ১. নির্দিষ্ট একটি ল্যাবের সব সার্ভিস লিস্টিং দেখা
        Task<IEnumerable<LabServiceListing>> GetByLabIdAsync(Guid labId, CancellationToken ct = default);

        // ২. ফিডের জন্য একটিভ সার্ভিসগুলো খুঁজে বের করা
        Task<IEnumerable<LabServiceListing>> GetActiveFeedServicesAsync(CancellationToken ct = default);

        // ৩. হোম কালেকশন এভেইলেবল আছে এমন সার্ভিসগুলো ফিল্টার করা
        Task<IEnumerable<LabServiceListing>> GetHomeCollectionServicesAsync(CancellationToken ct = default);

        // ৪. বাজেট অনুযায়ী সার্ভিস সার্চ করা (Price filtering)
        Task<IEnumerable<LabServiceListing>> GetServicesByPriceRangeAsync(decimal minPrice, decimal maxPrice, CancellationToken ct = default);

        // ৫. ফিড স্ট্যাটাস বাল্ক আপডেট করা (যেমন: সবগুলো একসাথে হাইড করা)
        Task UpdateFeedStatusAsync(Guid serviceId, FeedItemStatus status, CancellationToken ct = default);
    }
}

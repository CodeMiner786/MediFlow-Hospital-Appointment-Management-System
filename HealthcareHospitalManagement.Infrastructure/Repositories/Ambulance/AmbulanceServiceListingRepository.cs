using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using HealthcareHospitalManagement.Domain.Interfaces.Ambulance;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Ambulance
{
    // Primary Constructor ব্যবহার করে DbContext কে বেস ক্লাসে পাঠানো হয়েছে
    public class AmbulanceServiceListingRepository(ApplicationDbContext context)
        : GenericRepository<AmbulanceServiceListing>(context), IAmbulanceServiceListingRepository
    {
        // সরাসরি কুয়েরি করার জন্য DbSet ভেরিয়েবল
        private readonly DbSet<AmbulanceServiceListing> _dbSet = context.Set<AmbulanceServiceListing>();

        // ক্যাটাগরি অনুযায়ী সার্ভিসগুলো ফিল্টার করে স্ট্রীম আকারে রিটার্ন করা হচ্ছে
        public IAsyncEnumerable<AmbulanceServiceListing> GetServicesByCategoryStream(AmbulanceCategory category, CancellationToken ct = default)
        {
            return _dbSet
                // ক্যাটাগরি মিল আছে কিনা এবং ডাটা ডিলিট করা হয়নি কিনা তা চেক করা হচ্ছে
                .Where(s => s.Category == category && !s.IsDeleted)
                // শুধুমাত্র রিড করার জন্য ট্র্যাকিং অফ রাখা হয়েছে
                .AsNoTracking()
                // ডাটাবেজ থেকে ডেটাগুলো স্ট্রীম হিসেবে পাঠানো হচ্ছে
                .AsAsyncEnumerable();
        }

        // নির্দিষ্ট প্রোভাইডারের যেসব সার্ভিস বর্তমানে এভেইলঅ্যাবল আছে সেগুলো খুঁজে বের করা
        public async Task<IEnumerable<AmbulanceServiceListing>> GetAvailableServicesByProviderAsync(Guid providerId, CancellationToken ct = default)
        {
            return await _dbSet
                // প্রোভাইডার আইডি, এভেইলঅ্যাবিলিটি এবং ডিলিট স্ট্যাটাস চেক করা হচ্ছে
                .Where(s => s.ProviderId == providerId && s.IsAvailable && !s.IsDeleted)
                .ToListAsync(ct);
        }
    }
}

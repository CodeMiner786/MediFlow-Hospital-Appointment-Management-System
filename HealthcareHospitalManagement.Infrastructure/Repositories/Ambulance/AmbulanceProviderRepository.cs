using HealthcareHospitalManagement.Domain.Entities.Ambulance;
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
    // Primary Constructor এর মাধ্যমে DbContext ইনজেক্ট করা হয়েছে
    public class AmbulanceProviderProfileRepository(ApplicationDbContext context)
        : GenericRepository<AmbulanceProviderProfile>(context), IAmbulanceProviderProfileRepository
    {
        private readonly DbSet<AmbulanceProviderProfile> _dbSet = context.Set<AmbulanceProviderProfile>();

        // প্রোভাইডারের রেজিস্ট্রেশন নাম্বার দিয়ে তাকে খুঁজে বের করার ইমপ্লিমেন্টেশন
        public async Task<AmbulanceProviderProfile?> GetByRegistrationNumberAsync(string registrationNumber, CancellationToken ct)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => p.RegistrationNumber == registrationNumber && !p.IsDeleted, ct);
        }

        // IAsyncEnumerable ব্যবহার করে কোনো নির্দিষ্ট শহরের ভেরিফাইড প্রোভাইডারদের লিস্ট স্ট্রীম করা
        public IAsyncEnumerable<AmbulanceProviderProfile> GetVerifiedProvidersByCityStream(string city, CancellationToken ct)
        {
            return _dbSet
                // শহর এবং ভেরিফিকেশন স্ট্যাটাস চেক করা হচ্ছে, সাথে ডিলিট হওয়া রেকর্ডগুলো বাদ দেওয়া হয়েছে
                .Where(p => p.City == city && p.IsVerified && !p.IsDeleted)
                // মেমরি সেভ করার জন্য ট্র্যাকিং অফ রাখা হয়েছে (Read-only কুয়েরি)
                .AsNoTracking()
                // ডাটাবেজ থেকে ডেটাগুলো চাঙ্ক আকারে বা স্ট্রীম হিসেবে রিটার্ন করা হচ্ছে
                .AsAsyncEnumerable();
        }
    }
}

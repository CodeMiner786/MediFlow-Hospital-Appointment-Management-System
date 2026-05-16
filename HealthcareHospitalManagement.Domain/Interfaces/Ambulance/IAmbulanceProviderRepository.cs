using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Ambulance
{
    public interface IAmbulanceProviderProfileRepository : IGenericRepository<AmbulanceProviderProfile>
    {
        // প্রোভাইডারের রেজিস্ট্রেশন নাম্বার দিয়ে খোঁজা
        Task<AmbulanceProviderProfile?> GetByRegistrationNumberAsync(string registrationNumber, CancellationToken ct);

        // নির্দিষ্ট সিটির সব ভেরিফাইড প্রোভাইডার স্ট্রীম করা
        IAsyncEnumerable<AmbulanceProviderProfile> GetVerifiedProvidersByCityStream(string city, CancellationToken ct);
    }
}

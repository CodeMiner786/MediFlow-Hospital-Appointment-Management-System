using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.Interfaces.Ambulance
{
    public interface IAmbulanceServiceListingRepository : IGenericRepository<AmbulanceServiceListing>
    {
        // নির্দিষ্ট ক্যাটাগরি অনুযায়ী সার্ভিস লিস্ট স্ট্রীম করার মেথড
        IAsyncEnumerable<AmbulanceServiceListing> GetServicesByCategoryStream(AmbulanceCategory category, CancellationToken ct = default);

        // প্রোভাইডার আইডি দিয়ে তার সব এভেইলঅ্যাবল সার্ভিস খোঁজার মেথড
        Task<IEnumerable<AmbulanceServiceListing>> GetAvailableServicesByProviderAsync(Guid providerId, CancellationToken ct = default);
    }
}

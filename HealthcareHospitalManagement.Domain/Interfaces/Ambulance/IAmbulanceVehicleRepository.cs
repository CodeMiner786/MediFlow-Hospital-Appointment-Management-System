using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;

namespace HealthcareHospitalManagement.Domain.Interfaces.Ambulance;

public interface IAmbulanceVehicleRepository : IGenericRepository<AmbulanceVehicle>
{
    // 🔹 গাড়ির নম্বর দিয়ে খোঁজা
    Task<AmbulanceVehicle?> GetByVehicleNumberAsync(string vehicleNumber, CancellationToken ct = default);

    // 🔹 অ্যাম্বুলেন্স কোড দিয়ে খোঁজা
    Task<AmbulanceVehicle?> GetByAmbulanceCodeAsync(string ambulanceCode, CancellationToken ct = default);

    // 🔹 নির্দিষ্ট প্রোভাইডারের সব গাড়ি
    Task<IEnumerable<AmbulanceVehicle>> GetByProviderIdAsync(Guid providerId, CancellationToken ct = default);

    // 🔹 স্ট্যাটাস অনুযায়ী গাড়ি খোঁজা (Stream)
    IAsyncEnumerable<AmbulanceVehicle> GetVehiclesByStatusStream(AmbulanceStatus status, CancellationToken ct = default);

    // 🔹 ক্যাটাগরি অনুযায়ী গাড়ি খোঁজা
    Task<IEnumerable<AmbulanceVehicle>> GetByCategoryAsync(AmbulanceCategory category, CancellationToken ct = default);

    // 🔹 মেইনটেন্যান্স ডিউ আছে এমন গাড়ি
    Task<IEnumerable<AmbulanceVehicle>> GetVehiclesDueForMaintenanceAsync(DateTime targetDate, CancellationToken ct = default);
}
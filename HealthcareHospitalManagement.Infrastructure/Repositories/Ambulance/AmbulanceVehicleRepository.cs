using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.Enums.EmergencyAmbulance;
using HealthcareHospitalManagement.Domain.Interfaces.Ambulance;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Ambulance
{
    public class AmbulanceVehicleRepository(ApplicationDbContext context)
        : GenericRepository<AmbulanceVehicle>(context), IAmbulanceVehicleRepository
    {
        private readonly DbSet<AmbulanceVehicle> _dbSet = context.Set<AmbulanceVehicle>();

        // 🔹 গাড়ির নম্বর দিয়ে খোঁজা
        public async Task<AmbulanceVehicle?> GetByVehicleNumberAsync(string vehicleNumber, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(v => v.Provider)
                .FirstOrDefaultAsync(v => v.VehicleNumber == vehicleNumber && !v.IsDeleted, ct);
        }

        // 🔹 অ্যাম্বুলেন্স কোড দিয়ে খোঁজা
        public async Task<AmbulanceVehicle?> GetByAmbulanceCodeAsync(string ambulanceCode, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(v => v.Provider)
                .FirstOrDefaultAsync(v => v.AmbulanceCode == ambulanceCode && !v.IsDeleted, ct);
        }

        // 🔹 নির্দিষ্ট প্রোভাইডারের সব গাড়ি
        public async Task<IEnumerable<AmbulanceVehicle>> GetByProviderIdAsync(Guid providerId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(v => v.ProviderId == providerId && !v.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 স্ট্যাটাস অনুযায়ী গাড়ি খোঁজা (Available, OnDuty, etc.)
        public IAsyncEnumerable<AmbulanceVehicle> GetVehiclesByStatusStream(AmbulanceStatus status, CancellationToken ct = default)
        {
            return _dbSet
                .Where(v => v.Status == status && !v.IsDeleted)
                .AsAsyncEnumerable();
        }

        // 🔹 ক্যাটাগরি অনুযায়ী গাড়ি খোঁজা
        public async Task<IEnumerable<AmbulanceVehicle>> GetByCategoryAsync(AmbulanceCategory category, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(v => v.Category == category && !v.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 মেইনটেন্যান্স ডিউ আছে এমন গাড়ি
        public async Task<IEnumerable<AmbulanceVehicle>> GetVehiclesDueForMaintenanceAsync(DateTime targetDate, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(v => v.NextMaintenanceDue <= targetDate && !v.IsDeleted)
                .ToListAsync(ct);
        }
    }
}

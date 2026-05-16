using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.Interfaces.Ambulance;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Ambulance
{
    public class AmbulanceProviderWalletRepository(ApplicationDbContext context)
        : GenericRepository<AmbulanceProviderWallet>(context), IAmbulanceProviderWalletRepository
    {
        private readonly DbSet<AmbulanceProviderWallet> _dbSet = context.Set<AmbulanceProviderWallet>();

        public async Task<AmbulanceProviderWallet?> GetByProviderIdAsync(Guid providerId, CancellationToken ct = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(w => w.ProviderId == providerId && !w.IsDeleted, ct);
        }

        public async Task UpdateBalanceAsync(Guid providerId, decimal amount, CancellationToken ct = default)
        {
            var wallet = await GetByProviderIdAsync(providerId, ct);

            if (wallet != null)
            {
                wallet.Balance += amount;
                wallet.TotalEarned += amount > 0 ? amount : 0;
                wallet.LastTransactionAt = DateTime.UtcNow;

                // 🔹 এখন CancellationToken pass করা হচ্ছে
                await UpdateAsync(wallet, ct);
            }
        }
    }
}

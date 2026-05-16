using HealthcareHospitalManagement.Domain.Common.IGenericRepository;
using HealthcareHospitalManagement.Domain.Entities.Identity;

namespace HealthcareHospitalManagement.Domain.Interfaces.Identity
{
    public interface IUserLoginHistoryRepository : IGenericRepository<UserLoginHistory>
    {
        IAsyncEnumerable<UserLoginHistory> GetLogsByUserIdStream(Guid userId);
        Task<IEnumerable<UserLoginHistory>> GetRecentSuccessLoginsAsync(Guid userId, int count = 5);
        Task<int> GetFailedCountByIpAsync(string ipAddress, DateTime since);
        Task<IEnumerable<UserLoginHistory>> GetLogsByCountryAsync(string countryCode);
        Task DeleteOldHistoryAsync(DateTime beforeDate);
        // ✅ AddAsync সরানো হয়েছে
    }
}
using HealthcareHospitalManagement.Domain.Entities.Ambulance;
using HealthcareHospitalManagement.Domain.Interfaces.Ambulance;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Ambulance
{
    public class AmbulanceBookingRepository(ApplicationDbContext context)
        : GenericRepository<AmbulanceBooking>(context), IAmbulanceBookingRepository
    {
        private readonly DbSet<AmbulanceBooking> _dbSet = context.Set<AmbulanceBooking>();

        public async Task<AmbulanceBooking?> GetByBookingCodeAsync(string bookingCode, CancellationToken ct)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.BookingCode == bookingCode && !x.IsDeleted, ct);
        }

        public IAsyncEnumerable<AmbulanceBooking> GetActiveBookingsStream(CancellationToken ct)
        {
            return _dbSet
                .Where(x => !x.IsDeleted)
                .AsNoTracking()
                .AsAsyncEnumerable(); // এখানে CancellationToken সরাসরি pass করা যায় না
        }
    }
}

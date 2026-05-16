using HealthcareHospitalManagement.Domain.Entities.Wards;
using HealthcareHospitalManagement.Domain.Interfaces.Wards;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Wards
{
    public class BedBookingRepository(ApplicationDbContext context)
        : GenericRepository<BedBooking>(context), IBedBookingRepository
    {
        private readonly DbSet<BedBooking> _dbSet = context.Set<BedBooking>();

        // 🔹 বুকিং কোড দিয়ে বেড এবং ওয়ার্ডের তথ্যসহ লোড করা
        public async Task<BedBooking?> GetByBookingCodeAsync(string bookingCode, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(b => b.Bed)
                    .ThenInclude(bed => bed.Ward)
                .Include(b => b.Patient)
                .Include(b => b.BookedByUser)
                .FirstOrDefaultAsync(b => b.BookingCode == bookingCode && !b.IsDeleted, ct);
        }

        // 🔹 ইউজারের পার্সোনাল বুকিং হিস্টোরি
        public async Task<IEnumerable<BedBooking>> GetBookingsByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(b => b.BookedByUserId == userId && !b.IsDeleted)
                .Include(b => b.Bed)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync(ct);
        }

        // 🔹 পেশেন্ট অনুযায়ী ট্র্যাকিং
        public async Task<IEnumerable<BedBooking>> GetBookingsByPatientIdAsync(Guid patientId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(b => b.PatientId == patientId && !b.IsDeleted)
                .ToListAsync(ct);
        }

        // 🔹 পেন্ডিং পেমেন্টগুলো খুঁজে বের করা
        public async Task<IEnumerable<BedBooking>> GetBookingsByPaymentStatusAsync(bool isPaid, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(b => b.IsPaid == isPaid && !b.IsDeleted)
                .Include(b => b.BookedByUser)
                .ToListAsync(ct);
        }

        // 🔹 আজকের চেক-ইন লিস্ট
        public async Task<IEnumerable<BedBooking>> GetTodaysCheckInsAsync(CancellationToken ct = default)
        {
            var today = DateTime.UtcNow.Date;
            return await _dbSet
                .Where(b => b.CheckInDate.Date == today && !b.IsDeleted)
                .Include(b => b.Patient)
                .Include(b => b.Bed)
                .ToListAsync(ct);
        }

        // 🔹 পেমেন্ট গেটওয়ে রেসপন্স পাওয়ার পর স্ট্যাটাস আপডেট
        public async Task UpdatePaymentStatusAsync(Guid bookingId, bool status, CancellationToken ct = default)
        {
            var booking = await GetByIdAsync(bookingId, ct);
            if (booking != null)
            {
                booking.IsPaid = status;
                await context.SaveChangesAsync(ct);
            }
        }
    }
}

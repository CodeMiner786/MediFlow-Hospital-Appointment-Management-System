using HealthcareHospitalManagement.Domain.Entities.Lab;
using HealthcareHospitalManagement.Domain.Enums.Lab;
using HealthcareHospitalManagement.Domain.Interfaces.Lab;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Lab
{
    public class LabOrderRepository(ApplicationDbContext context)
        : GenericRepository<LabOrder>(context), ILabOrderRepository
    {
        private readonly DbSet<LabOrder> _dbSet = context.Set<LabOrder>();

        // 🔹 অর্ডার কোড দিয়ে সার্চ
        public async Task<LabOrder?> GetByOrderCodeAsync(string orderCode)
        {
            return await _dbSet
                .Include(o => o.Patient)
                .Include(o => o.OrderedByDoctor)
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.OrderCode == orderCode && !o.IsDeleted);
        }

        // 🔹 পেশেন্টের সব ল্যাব হিস্টোরি
        public async Task<IEnumerable<LabOrder>> GetOrdersByPatientIdAsync(Guid patientId)
        {
            return await _dbSet
                .Where(o => o.PatientId == patientId && !o.IsDeleted)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        // 🔹 ডক্টরের রেফার করা পেশেন্টদের ল্যাব লিস্ট
        public async Task<IEnumerable<LabOrder>> GetOrdersByDoctorIdAsync(Guid doctorId)
        {
            return await _dbSet
                .Where(o => o.OrderedByDoctorId == doctorId && !o.IsDeleted)
                .Include(o => o.Patient)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        // 🔹 পেমেন্ট স্ট্যাটাস ফিল্টার
        public async Task<IEnumerable<LabOrder>> GetOrdersByPaymentStatusAsync(bool isPaid)
        {
            return await _dbSet
                .Where(o => o.IsPaid == isPaid && !o.IsDeleted)
                .Include(o => o.Patient)
                .ToListAsync();
        }

        // 🔹 আর্জেন্ট টেস্ট
        public async Task<IEnumerable<LabOrder>> GetUrgentOrdersAsync()
        {
            return await _dbSet
                .Where(o => o.Priority == TestPriority.Urgent && !o.IsDeleted)
                .Include(o => o.Patient)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        // 🔹 ডেট রেঞ্জ ফিল্টার
        public async Task<IEnumerable<LabOrder>> GetOrdersByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _dbSet
                .Where(o => o.OrderDate >= start && o.OrderDate <= end && !o.IsDeleted)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        // 🔹 Filtered and paginated
        public async Task<List<LabOrder>> GetFilteredOrdersAsync(
            Guid? patientId, Guid? doctorId, TestPriority? priority,
            bool? isPaid, DateTime? dateFrom, DateTime? dateTo,
            int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var query = _dbSet.AsQueryable();

            if (patientId.HasValue)
                query = query.Where(x => x.PatientId == patientId.Value);
            if (doctorId.HasValue)
                query = query.Where(x => x.OrderedByDoctorId == doctorId.Value);
            if (priority.HasValue)
                query = query.Where(x => x.Priority == priority.Value);
            if (isPaid.HasValue)
                query = query.Where(x => x.IsPaid == isPaid.Value);
            if (dateFrom.HasValue)
                query = query.Where(x => x.OrderDate >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.OrderDate <= dateTo.Value);

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }

        // ✅ নতুন — Single Order with full details (Patient, Doctor, LabProfile, Items+LabTest)
        public async Task<LabOrder?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(o => o.Patient)
                .Include(o => o.OrderedByDoctor)
                .Include(o => o.LabProfile)
                .Include(o => o.Items)
                    .ThenInclude(i => i.LabTest)
                .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted, ct);
        }

        // ✅ নতুন — Patient এর সব Orders with full details
        public async Task<IEnumerable<LabOrder>> GetByPatientIdAsync(Guid patientId, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(o => o.Patient)
                .Include(o => o.OrderedByDoctor)
                .Include(o => o.LabProfile)
                .Include(o => o.Items)
                    .ThenInclude(i => i.LabTest)
                .Where(o => o.PatientId == patientId && !o.IsDeleted)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync(ct);
        }
    }
}
using HealthcareHospitalManagement.Domain.Entities.Pharmacy;
using HealthcareHospitalManagement.Domain.Interfaces.Pharmacy;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Pharmacy
{
    public class MedicineOrderItemRepository(ApplicationDbContext context)
        : GenericRepository<MedicineOrderItem>(context), IMedicineOrderItemRepository
    {
        private readonly DbSet<MedicineOrderItem> _dbSet = context.Set<MedicineOrderItem>();

        public async Task<IEnumerable<MedicineOrderItem>> GetItemsByOrderIdAsync(Guid orderId, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(i => i.Medicine)
                .Where(i => i.MedicineOrderId == orderId && !i.IsDeleted)
                .ToListAsync(ct);   // 🔹 ct pass করা হলো
        }

        public async Task<IEnumerable<MedicineOrderItem>> GetItemsByMedicineIdAsync(Guid medicineId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(i => i.MedicineId == medicineId && !i.IsDeleted)
                .Include(i => i.MedicineOrder)
                .ToListAsync(ct);   // 🔹 ct pass করা হলো
        }

        public async Task<decimal> GetTotalDiscountByOrderIdAsync(Guid orderId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(i => i.MedicineOrderId == orderId && !i.IsDeleted)
                .SumAsync(i => i.Discount, ct);   // 🔹 ct pass করা হলো
        }

        public async Task<MedicineOrderItem?> GetItemByOrderAndMedicineIdAsync(Guid orderId, Guid medicineId, CancellationToken ct = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(i => i.MedicineOrderId == orderId &&
                                          i.MedicineId == medicineId &&
                                          !i.IsDeleted, ct);   // 🔹 ct pass করা হলো
        }
    }
}

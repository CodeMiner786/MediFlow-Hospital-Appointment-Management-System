using HealthcareHospitalManagement.Domain.Entities.Payment;
using HealthcareHospitalManagement.Domain.Enums.BillingPayment;
using HealthcareHospitalManagement.Domain.Interfaces.Payment;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HealthcareHospitalManagement.Infrastructure.Repositories.Payment
{
    public class PaymentTransactionRepository(ApplicationDbContext context)
        : GenericRepository<PaymentTransaction>(context), IPaymentTransactionRepository
    {
        private readonly DbSet<PaymentTransaction> _dbSet = context.Set<PaymentTransaction>();

        public async Task<PaymentTransaction?> GetByTransactionIdAsync(string transactionId, CancellationToken ct = default)
        {
            return await _dbSet.FirstOrDefaultAsync(t => t.TransactionId == transactionId && !t.IsDeleted, ct);
        }

        public async Task<IEnumerable<PaymentTransaction>> GetTransactionsByBillIdAsync(Guid billId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(t => t.BillId == billId && !t.IsDeleted)
                .OrderByDescending(t => t.PaymentDate)
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<PaymentTransaction>> GetTransactionsByStatusAsync(PaymentStatus status, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(t => t.Status == status && !t.IsDeleted)
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<PaymentTransaction>> GetTransactionsByGatewayAsync(PaymentGateway gateway, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(t => t.Gateway == gateway && !t.IsDeleted)
                .ToListAsync(ct);
        }

        public async Task UpdateRefundStatusAsync(Guid transactionId, string reason, string refundTxnId, CancellationToken ct = default)
        {
            var transaction = await GetByIdAsync(transactionId, ct);
            if (transaction != null)
            {
                transaction.IsRefunded = true;
                transaction.RefundReason = reason;
                transaction.RefundTransactionId = refundTxnId;
                transaction.RefundedAt = DateTime.UtcNow;
                transaction.Status = PaymentStatus.Refunded;

                await context.SaveChangesAsync(ct);
            }
        }

        public async Task<decimal> GetTotalRevenueByDateRangeAsync(DateTime start, DateTime end, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(t => t.Status == PaymentStatus.Successful
                            && t.PaymentDate >= start
                            && t.PaymentDate <= end
                            && !t.IsDeleted)
                .SumAsync(t => t.Amount, ct);
        }
    }
}

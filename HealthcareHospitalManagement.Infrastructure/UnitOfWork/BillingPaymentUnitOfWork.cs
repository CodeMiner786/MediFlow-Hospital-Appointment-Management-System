using HealthcareHospitalManagement.Domain.Interfaces.Billing;
using HealthcareHospitalManagement.Domain.Interfaces.Payment;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using HealthcareHospitalManagement.Infrastructure.DatabaseContext.Db;
using HealthcareHospitalManagement.Infrastructure.Repositories.Billing;
using HealthcareHospitalManagement.Infrastructure.Repositories.Payment;

namespace HealthcareHospitalManagement.Infrastructure.UnitOfWork
{
    public class BillingPaymentUnitOfWork(ApplicationDbContext context) : IBillingPaymentUnitOfWork
    {
        // Billing
        public IBillRepository Bills { get; } = new BillRepository(context);
        public IBillItemRepository BillItems { get; } = new BillItemRepository(context);
        public IInsuranceClaimRepository InsuranceClaims { get; } = new InsuranceClaimRepository(context);
        public IInvoiceRepository Invoices { get; } = new InvoiceRepository(context);

        // Payment
        public IPaymentTransactionRepository PaymentTransactions { get; } = new PaymentTransactionRepository(context);
        public IPlatformWalletRepository PlatformWallets { get; } = new PlatformWalletRepository(context);
        public IPlatformWalletTransactionRepository PlatformWalletTransactions { get; } = new PlatformWalletTransactionRepository(context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await context.SaveChangesAsync(cancellationToken);

        public void Dispose() => context.Dispose();
    }

}

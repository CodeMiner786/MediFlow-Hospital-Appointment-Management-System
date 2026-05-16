using HealthcareHospitalManagement.Domain.Interfaces.Billing;
using HealthcareHospitalManagement.Domain.Interfaces.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork
{
    public interface IBillingPaymentUnitOfWork : IDisposable
    {
        // Billing
        IBillRepository Bills { get; }
        IBillItemRepository BillItems { get; }
        IInsuranceClaimRepository InsuranceClaims { get; }
        IInvoiceRepository Invoices { get; }

        // Payment
        IPaymentTransactionRepository PaymentTransactions { get; }
        IPlatformWalletRepository PlatformWallets { get; }
        IPlatformWalletTransactionRepository PlatformWalletTransactions { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}

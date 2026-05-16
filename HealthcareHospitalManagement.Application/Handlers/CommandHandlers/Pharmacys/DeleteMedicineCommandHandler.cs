using HealthcareHospitalManagement.Application.Commands.Pharmacys;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Pharmacys
{
    public sealed class DeleteMedicineCommandHandler(IPharmacyUnitOfWork uow)
     : IRequestHandler<DeleteMedicineCommand, ApiResponseDto<bool>>
    {
        public async Task<ApiResponseDto<bool>> Handle(
            DeleteMedicineCommand request,
            CancellationToken cancellationToken)
        {
            var medicine = await uow.Medicines.GetByIdAsync(request.MedicineId, cancellationToken);
            if (medicine is null)
                return ApiResponseDto<bool>.Fail(
                    $"MedicineId '{request.MedicineId}' পাওয়া যায়নি।");

            // ── Soft Delete ──
            medicine.IsDeleted = true;
            medicine.DeletedAt = DateTime.UtcNow;

            await uow.Medicines.UpdateAsync(medicine, cancellationToken);
            await uow.SaveChangesAsync(cancellationToken);

            return ApiResponseDto<bool>.Ok(true, "Medicine সফলভাবে ডিলিট করা হয়েছে।");
        }
    }

}

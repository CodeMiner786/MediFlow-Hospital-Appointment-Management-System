using HealthcareHospitalManagement.Application.Commands.Patients;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.Patients
{
    public sealed class DeletePatientCommandHandler(IPatientUnitOfWork uow)
        : IRequestHandler<DeletePatientCommand, bool>
    {
        public async Task<bool> Handle(
            DeletePatientCommand request,
            CancellationToken cancellationToken)
        {
            // ── Patient Validate ──
            var patient = await uow.Patients.GetByIdAsync(request.PatientId, cancellationToken)
                ?? throw new KeyNotFoundException(
                    $"PatientId '{request.PatientId}' পাওয়া যায়নি।");

            // ── Soft Delete ──
            patient.IsDeleted = true;
            patient.DeletedAt = DateTime.UtcNow;

            // ── Update with CancellationToken ──
            await uow.Patients.UpdateAsync(patient, cancellationToken);

            // ── SaveChanges with CancellationToken ──
            await uow.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.DoctorFeedbackSummary;
using HealthcareHospitalManagement.Application.DTOs.DoctorFeedbackSummary;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorFeedbackSummary
{
    public class CreateDoctorFeedbackSummaryHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateDoctorFeedbackSummaryCommand, ApiResponseDto<DoctorFeedbackSummaryDto>>
    {
        public async Task<ApiResponseDto<DoctorFeedbackSummaryDto>> Handle(
            CreateDoctorFeedbackSummaryCommand request, CancellationToken ct)
        {
            var existing = await uow.DoctorFeedbackSummaries.GetByDoctorIdAsync(request.Dto.DoctorId, ct);
            if (existing is not null)
                return ApiResponseDto<DoctorFeedbackSummaryDto>.FailResponse(
                    "Feedback summary already exists for this doctor.");

            var entity = mapper.Map<Domain.Entities.Doctor.DoctorFeedbackSummary>(request.Dto);
            entity.LastUpdatedAt = DateTime.UtcNow;

            await uow.DoctorFeedbackSummaries.AddAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<DoctorFeedbackSummaryDto>.SuccessResponse(
                mapper.Map<DoctorFeedbackSummaryDto>(entity), "Feedback summary created successfully.");
        }
    }

}

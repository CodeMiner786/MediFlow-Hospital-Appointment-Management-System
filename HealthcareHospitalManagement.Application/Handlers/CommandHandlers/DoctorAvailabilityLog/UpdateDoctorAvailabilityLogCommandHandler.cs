using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.DoctorAvailabilityLog;
using HealthcareHospitalManagement.Application.DTOs.DoctorAvailabilityLog;
using HealthcareHospitalManagement.Domain.Interfaces.Doctor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.DoctorAvailabilityLog
{
    // ক্লাসের নামের সাথেই ডিপেন্ডেন্সিগুলো প্রাইমারি কনস্ট্রাক্টরে রিসিভ করা হলো
    public sealed class UpdateDoctorAvailabilityLogCommandHandler(
        IDoctorAvailabilityLogRepository repository,
        IMapper mapper)
        : IRequestHandler<UpdateDoctorAvailabilityLogCommand, ApiResponseDto<DoctorAvailabilityLogDto>>
    {
        public async Task<ApiResponseDto<DoctorAvailabilityLogDto>> Handle(
            UpdateDoctorAvailabilityLogCommand request,
            CancellationToken cancellationToken)
        {
            // আলাদা ফিল্ড ছাড়া সরাসরি 'repository' ব্যবহার করা হয়েছে
            var entity = await repository.GetByIdAsync(request.LogId, cancellationToken);
            if (entity is null)
                return ApiResponseDto<DoctorAvailabilityLogDto>.FailResponse($"Log with Id '{request.LogId}' not found.");

            // সরাসরি 'mapper' ব্যবহার করে রিকোয়েস্টের ডাটা এন্টিটিতে ম্যাপ করা হলো
            mapper.Map(request.Dto, entity);

            await repository.UpdateAsync(entity, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            var responseDto = mapper.Map<DoctorAvailabilityLogDto>(entity);
            return ApiResponseDto<DoctorAvailabilityLogDto>.SuccessResponse(responseDto, "Log successfully updated.");
        }
    }
}
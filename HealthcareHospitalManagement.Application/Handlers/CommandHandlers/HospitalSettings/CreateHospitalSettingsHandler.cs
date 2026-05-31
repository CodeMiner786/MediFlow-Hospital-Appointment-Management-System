using AutoMapper;
using HealthcareHospitalManagement.Application.Commands.HospitalSettings;
using HealthcareHospitalManagement.Application.DTOs.HospitalSettings;
using HealthcareHospitalManagement.Domain.IServiceRegistrar.IUnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Handlers.CommandHandlers.HospitalSettings
{
    public class CreateHospitalSettingsHandler(IDoctorUnitOfWork uow, IMapper mapper)
    : IRequestHandler<CreateHospitalSettingsCommand, ApiResponseDto<HospitalSettingsDto>>
    {
        public async Task<ApiResponseDto<HospitalSettingsDto>> Handle(
            CreateHospitalSettingsCommand request, CancellationToken ct)
        {
            // শুধুমাত্র একটি সেটিংস রেকর্ড থাকবে
            var existing = await uow.HospitalSettings.GetCurrentSettingsAsync(ct);
            if (existing is not null)
                return ApiResponseDto<HospitalSettingsDto>.FailResponse(
                    "Hospital settings already exist. Use update instead.");

            // শেয়ার পারসেন্টেজ মোট ১০০ হওয়া চেক
            if (request.Dto.DefaultPlatformSharePercent + request.Dto.DefaultDoctorSharePercent != 100)
                return ApiResponseDto<HospitalSettingsDto>.FailResponse(
                    "Platform and doctor share percentages must sum to 100.");

            var entity = mapper.Map<Domain.Entities.Doctor.HospitalSettings>(request.Dto);

            await uow.HospitalSettings.AddAsync(entity, ct);
            await uow.SaveChangesAsync(ct);

            return ApiResponseDto<HospitalSettingsDto>.SuccessResponse(
                mapper.Map<HospitalSettingsDto>(entity), "Hospital settings created successfully.");
        }
    }

}

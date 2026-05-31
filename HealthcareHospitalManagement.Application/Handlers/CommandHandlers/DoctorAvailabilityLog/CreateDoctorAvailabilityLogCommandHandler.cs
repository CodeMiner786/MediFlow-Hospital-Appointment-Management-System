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
    // ক্লাসের নামের পাশেই সরাসরি প্রাইমারি কনস্ট্রাক্টর দিয়ে ডিপেন্ডেন্সিগুলো রিসিভ করা হয়েছে
    public sealed class CreateDoctorAvailabilityLogCommandHandler(
        IDoctorAvailabilityLogRepository repository,
        IMapper mapper)
        : IRequestHandler<CreateDoctorAvailabilityLogCommand, ApiResponseDto<DoctorAvailabilityLogDto>>
    {
        // আলাদা ফিল্ড ডিক্লেয়ার ও অ্যাসাইন করার দরকার নেই, কনস্ট্রাক্টরের প্যারামিটারগুলোকেই এখানে রিড-অনলি ফিল্ড হিসেবে রাখা হয়েছে
        private readonly IDoctorAvailabilityLogRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponseDto<DoctorAvailabilityLogDto>> Handle(
            CreateDoctorAvailabilityLogCommand request,
            CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Doctor.DoctorAvailabilityLog>(request.Dto);

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            var responseDto = _mapper.Map<DoctorAvailabilityLogDto>(entity);
            return ApiResponseDto<DoctorAvailabilityLogDto>.SuccessResponse(responseDto, "Log successfully created.");
        }
    }
}
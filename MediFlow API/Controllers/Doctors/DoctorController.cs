using HealthcareHospitalManagement.Application.Commands.Doctors;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Doctor;
using HealthcareHospitalManagement.Application.Queries.Doctors;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Doctors
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController(ISender mediator) : ControllerBase
    {
        // 1. Create Doctor
        [Authorize] // 🔹 Sensitive
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), 200)]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorRequestDto dto)
        {
            var doctorId = await mediator.Send(new CreateDoctorCommand(dto));
            return Ok(ApiResponse<Guid>.SuccessResponse(doctorId, "Doctor profile created successfully."));
        }

        // 2. Get Departments
        [AllowAnonymous] // 🔹 Public
        [HttpGet("departments")]
        [ProducesResponseType(typeof(ApiResponse<PagedResultDto<DepartmentResponseDto>>), 200)]
        public async Task<IActionResult> GetDepartments(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await mediator.Send(new GetDepartmentsQuery(pageNumber, pageSize));
            return Ok(ApiResponse<PagedResultDto<DepartmentResponseDto>>.SuccessResponse(result, "Department list retrieved successfully."));
        }

        // 3. Get Doctor Detail
        [AllowAnonymous] // 🔹 Public
        [HttpGet("detail/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<DoctorDetailResponseDto>), 200)]
        public async Task<IActionResult> GetDoctorDetail(Guid id)
        {
            var result = await mediator.Send(new GetDoctorDetailQuery(id));
            return Ok(ApiResponse<DoctorDetailResponseDto>.SuccessResponse(result, "Doctor detail retrieved successfully."));
        }

        // 4. Create Doctor Leave
        [Authorize] // 🔹 Sensitive
        [HttpPost("leave")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), 200)]
        public async Task<IActionResult> CreateDoctorLeave([FromBody] DoctorLeaveRequestDto dto)
        {
            var result = await mediator.Send(new CreateDoctorLeaveCommand(dto));
            return Ok(ApiResponse<Guid>.SuccessResponse(result, "Doctor leave request created successfully."));
        }

        // 5. Create Doctor Schedule
        [Authorize] // 🔹 Sensitive
        [HttpPost("schedule")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), 200)]
        public async Task<IActionResult> CreateDoctorSchedule([FromBody] DoctorScheduleRequestDto dto)
        {
            var result = await mediator.Send(new CreateDoctorScheduleCommand(dto));
            return Ok(ApiResponse<Guid>.SuccessResponse(result, "Doctor schedule created successfully."));
        }

        // 6. Get Doctor Schedule Slots
        [AllowAnonymous] // 🔹 Public
        [HttpGet("{doctorId:guid}/schedule-slots")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<DoctorScheduleSlotResponseDto>>), 200)]
        public async Task<IActionResult> GetDoctorScheduleSlots(
            Guid doctorId,
            [FromQuery] DayOfWeek dayOfWeek)
        {
            var result = await mediator.Send(new GetDoctorScheduleSlotsQuery(doctorId, dayOfWeek));
            return Ok(ApiResponse<IEnumerable<DoctorScheduleSlotResponseDto>>.SuccessResponse(result, "Doctor schedule slots retrieved successfully."));
        }

        // 7. Search Doctors
        [AllowAnonymous] // 🔹 Public
        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiResponse<PagedResultDto<DoctorSummaryResponseDto>>), 200)]
        public async Task<IActionResult> SearchDoctors(
            [FromQuery] DoctorSearchRequestDto dto,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetDoctorsQuery(dto), cancellationToken);
            return Ok(ApiResponse<PagedResultDto<DoctorSummaryResponseDto>>.SuccessResponse(result, "Doctor search results retrieved successfully."));
        }

        // 8. Update Doctor
        [Authorize] // 🔹 Sensitive
        [HttpPut("update/{doctorId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), 200)]
        public async Task<IActionResult> UpdateDoctor(
            Guid doctorId,
            [FromBody] UpdateDoctorRequestDto dto,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new UpdateDoctorCommand(doctorId, dto), cancellationToken);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}

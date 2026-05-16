using HealthcareHospitalManagement.Application.DTOs.Appointment;
using HealthcareHospitalManagement.Application.Queries.Appointments;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HealthcareHospitalManagement.Application.Commands.Appointments;
using HealthcareHospitalManagement.Application.Common;

namespace MediFlow_API.Controllers.Appointments
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController(IMediator mediator) : ControllerBase
    {
        // ══════════════════════════════════════════════════════
        //  GET Endpoints (Public)
        // ══════════════════════════════════════════════════════

        [HttpGet("{id:guid}")]
        [AllowAnonymous] // 🔹 Public
        public async Task<ActionResult<ApiResponse<AppointmentDetailResponseDto>>> GetAppointmentDetail(Guid id)
        {
            var result = await mediator.Send(new GetAppointmentDetailQuery(id));

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous] // 🔹 Public
        public async Task<ActionResult<ApiResponse<PagedResultDto<AppointmentSummaryResponseDto>>>> GetAppointments(
            [FromQuery] AppointmentFilterRequestDto filter)
        {
            var result = await mediator.Send(new GetAppointmentsQuery(filter));

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("today-queue")]
        [AllowAnonymous] // 🔹 Public
        public async Task<ActionResult<ApiResponse<List<TodayQueueItemResponseDto>>>> GetTodayQueue([FromQuery] DateTime date)
        {
            var result = await mediator.Send(new GetTodayQueueQuery(date));

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // ══════════════════════════════════════════════════════
        //  POST Endpoints (Authorize Required)
        // ══════════════════════════════════════════════════════

        [HttpPost("book")]
        [Authorize] // 🔹 Sensitive
        public async Task<ActionResult<ApiResponse<Guid>>> BookAppointment([FromBody] BookAppointmentRequestDto dto)
        {
            var result = await mediator.Send(new BookAppointmentCommand(dto));
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("cancel")]
        [Authorize] // 🔹 Sensitive
        public async Task<ActionResult<ApiResponse<bool>>> CancelAppointment([FromBody] CancelAppointmentRequestDto dto)
        {
            var result = await mediator.Send(new CancelAppointmentCommand(dto));

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost("reschedule")]
        [Authorize] // 🔹 Sensitive
        public async Task<ActionResult<ApiResponse<bool>>> RescheduleAppointment([FromBody] RescheduleAppointmentRequestDto dto)
        {
            var result = await mediator.Send(new RescheduleAppointmentCommand(dto));

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}

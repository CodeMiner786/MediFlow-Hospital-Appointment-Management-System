using HealthcareHospitalManagement.Application.Commands.Telemedicines;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Telemedicine;
using HealthcareHospitalManagement.Application.Queries.Telemedicines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Telemedicines
{
    [ApiController]
    [Route("api/telemedicine")]
    public class TelemedicineController(IMediator mediator) : ControllerBase
    {
        // ══════════════════════════════════════════════════════
        //  QUERY ENDPOINTS — GET
        // ══════════════════════════════════════════════════════

        /// GET /api/telemedicine/sessions?pageNumber=1&pageSize=10
        [HttpGet("sessions")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<TelemedicineSessionResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetAllTelemedicineSessionsQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        /// GET /api/telemedicine/sessions/upcoming?pageNumber=1&pageSize=10
        [HttpGet("sessions/upcoming")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<TelemedicineSessionResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUpcoming(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetUpcomingSessionsQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        /// GET /api/telemedicine/sessions/{id}
        [HttpGet("sessions/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<TelemedicineSessionResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetTelemedicineSessionByIdQuery(id), ct);
            return Ok(result);
        }

        /// GET /api/telemedicine/sessions/patient/{patientId}?pageNumber=1&pageSize=10
        [HttpGet("sessions/patient/{patientId:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<TelemedicineSessionResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByPatient(
            Guid patientId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetSessionsByPatientQuery(patientId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        /// GET /api/telemedicine/sessions/doctor/{doctorId}?pageNumber=1&pageSize=10
        [HttpGet("sessions/doctor/{doctorId:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<TelemedicineSessionResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByDoctor(
            Guid doctorId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetSessionsByDoctorQuery(doctorId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ══════════════════════════════════════════════════════
        //  COMMAND ENDPOINTS — POST, PUT, DELETE
        // ══════════════════════════════════════════════════════

        /// POST /api/telemedicine/sessions
        [HttpPost("sessions")]
        [ProducesResponseType(typeof(ApiResponseDto<TelemedicineSessionResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateTelemedicineSessionRequestDto dto,
            CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await mediator.Send(new CreateTelemedicineSessionCommand(dto), ct);
            return Ok(result);
        }

        /// PUT /api/telemedicine/sessions/{id}/start
        [HttpPut("sessions/{id:guid}/start")]
        [ProducesResponseType(typeof(ApiResponseDto<TelemedicineSessionResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Start(
            Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new StartTelemedicineSessionCommand(id), ct);
            return Ok(result);
        }

        /// PUT /api/telemedicine/sessions/end
        [HttpPut("sessions/end")]
        [ProducesResponseType(typeof(ApiResponseDto<TelemedicineSessionResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> End(
            [FromBody] EndSessionRequestDto dto,
            CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await mediator.Send(new EndTelemedicineSessionCommand(dto), ct);
            return Ok(result);
        }

        /// DELETE /api/telemedicine/sessions/{id}/cancel
        [HttpDelete("sessions/{id:guid}/cancel")]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Cancel(
            Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new CancelTelemedicineSessionCommand(id), ct);
            return Ok(result);
        }
    }

}

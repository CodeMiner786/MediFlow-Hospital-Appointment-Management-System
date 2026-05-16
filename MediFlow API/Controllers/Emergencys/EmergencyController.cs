using HealthcareHospitalManagement.Application.Commands.Emergencys;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Emergency;
using HealthcareHospitalManagement.Application.Queries.Emergencys;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Emergencys
{
    [ApiController]
    [Route("api/emergency")]
    public class EmergencyController(IMediator mediator) : ControllerBase
    {
        // ══════════════════════════════════════════════════════
        //  QUERY ENDPOINTS — GET (Public)
        // ══════════════════════════════════════════════════════

        [HttpGet]
        [AllowAnonymous] // 🔹 Public
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<EmergencyVisitResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetAllEmergencyVisitsQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        [HttpGet("active")]
        [AllowAnonymous] // 🔹 Public
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<EmergencyVisitResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActive(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetActiveEmergencyVisitsQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        [HttpGet("filter")]
        [AllowAnonymous] // 🔹 Public
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<EmergencyVisitResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByFilter(
            [FromQuery] EmergencyVisitFilterRequestDto dto,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetEmergencyVisitsByFilterQuery(dto), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous] // 🔹 Public
        [ProducesResponseType(typeof(ApiResponseDto<EmergencyVisitResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(
            Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetEmergencyVisitByIdQuery(id), ct);
            return Ok(result);
        }

        [HttpGet("patient/{patientId:guid}")]
        [AllowAnonymous] // 🔹 Public
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<EmergencyVisitResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByPatient(
            Guid patientId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetEmergencyVisitsByPatientQuery(patientId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ══════════════════════════════════════════════════════
        //  COMMAND ENDPOINTS — POST, PUT (Authorize Required)
        // ══════════════════════════════════════════════════════

        [HttpPost]
        [Authorize] // 🔹 Sensitive
        [ProducesResponseType(typeof(ApiResponseDto<EmergencyVisitResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateEmergencyVisitRequestDto dto,
            CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await mediator.Send(new CreateEmergencyVisitCommand(dto), ct);
            return Ok(result);
        }

        [HttpPut("update")]
        [Authorize] // 🔹 Sensitive
        [ProducesResponseType(typeof(ApiResponseDto<EmergencyVisitResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromBody] UpdateEmergencyVisitRequestDto dto,
            CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await mediator.Send(new UpdateEmergencyVisitCommand(dto), ct);
            return Ok(result);
        }

        [HttpPut("{id:guid}/discharge")]
        [Authorize] // 🔹 Sensitive
        [ProducesResponseType(typeof(ApiResponseDto<EmergencyVisitResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Discharge(
            Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new DischargeEmergencyPatientCommand(id), ct);
            return Ok(result);
        }
    }
}

using HealthcareHospitalManagement.Application.Commands.Wards;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Wards;
using HealthcareHospitalManagement.Application.Queries.Words;
using HealthcareHospitalManagement.Domain.Enums.WardBed;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Wards
{
    [ApiController]
    [Route("api/wards")]
    public class WardController(IMediator mediator) : ControllerBase
    {
        // ══════════════════════════════════════════════════════
        //  WARD ENDPOINTS (Public)
        // ══════════════════════════════════════════════════════

        /// GET /api/wards?pageNumber=1&pageSize=10
        [HttpGet]
        [AllowAnonymous] // 🔹 Public
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<WardResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllWards(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetAllWardsQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        /// GET /api/wards/available?pageNumber=1&pageSize=10
        [HttpGet("available")]
        [AllowAnonymous] // 🔹 Public
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<WardResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWardsWithAvailableBeds(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetWardsWithAvailableBedsQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ══════════════════════════════════════════════════════
        //  BED ENDPOINTS (Authorize Required)
        // ══════════════════════════════════════════════════════

        [HttpGet("{wardId:guid}/beds")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<BedResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBedsByWard(Guid wardId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetBedsByWardQuery(wardId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        [HttpGet("{wardId:guid}/beds/available")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<BedResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailableBeds(Guid wardId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetAvailableBedsQuery(wardId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        [HttpGet("beds/filter")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<BedResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBedsByFilter([FromQuery] BedFilterRequestDto dto,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetBedsByFilterQuery(dto), ct);
            return Ok(result);
        }

        [HttpPut("beds/{bedId:guid}/status")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBedStatus(Guid bedId,
            [FromQuery] BedStatus newStatus,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new UpdateBedStatusCommand(bedId, newStatus), ct);
            return Ok(result);
        }

        // ══════════════════════════════════════════════════════
        //  ADMISSION ENDPOINTS (Authorize Required)
        // ══════════════════════════════════════════════════════

        [HttpGet("admissions/{id:guid}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseDto<AdmissionResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdmissionById(Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetAdmissionByIdQuery(id), ct);
            return Ok(result);
        }

        [HttpGet("admissions/current")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<AdmissionResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentlyAdmittedPatients(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetCurrentlyAdmittedPatientsQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        [HttpGet("admissions/patient/{patientId:guid}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<AdmissionResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPatientAdmissionHistory(Guid patientId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetPatientAdmissionHistoryQuery(patientId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        [HttpGet("admissions/doctor/{doctorId:guid}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<AdmissionResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdmissionsByDoctor(Guid doctorId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetAdmissionsByDoctorQuery(doctorId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        [HttpPost("admissions")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseDto<AdmissionResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAdmission([FromBody] CreateAdmissionRequestDto dto,
            CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await mediator.Send(new CreateAdmissionCommand(dto), ct);
            return Ok(result);
        }

        [HttpPost("admissions/discharge")]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponseDto<AdmissionResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DischargePatient([FromBody] DischargePatientRequestDto dto,
            CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await mediator.Send(new DischargePatientCommand(dto), ct);
            return Ok(result);
        }
    }
}

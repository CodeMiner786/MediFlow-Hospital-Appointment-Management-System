using HealthcareHospitalManagement.Application.Commands.Patients;
using HealthcareHospitalManagement.Application.DTOs.Patient;
using HealthcareHospitalManagement.Application.Queries.Patients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Patients;

[ApiController]
[Route("api/patients")]
public class PatientController(IMediator mediator) : ControllerBase
{
    // ══════════════════════════════════════════════════════
    //  GET Endpoints
    // ══════════════════════════════════════════════════════

    [HttpGet]
    [AllowAnonymous] // 🔹 Public
    [ProducesResponseType(typeof(IEnumerable<PatientSummaryResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetAllPatientsQuery(pageNumber, pageSize), ct);
        return Ok(result);
    }

    [HttpGet("search")]
    [AllowAnonymous] // 🔹 Public
    [ProducesResponseType(typeof(IEnumerable<PatientSummaryResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search(
        [FromQuery] PatientSearchRequestDto dto,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new SearchPatientsQuery(dto), ct);
        return Ok(result);
    }

    [HttpGet("my-profile")]
    [Authorize] // 🔹 Sensitive
    [ProducesResponseType(typeof(PatientDetailResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMyProfile(
        [FromQuery] Guid userId,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetPatientByUserIdQuery(userId), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [Authorize] // 🔹 Sensitive
    [ProducesResponseType(typeof(PatientSummaryResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetPatientByIdQuery(id), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}/profile")]
    [Authorize] // 🔹 Sensitive
    [ProducesResponseType(typeof(PatientDetailResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFullProfile(Guid id, CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetPatientFullProfileQuery(id), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}/vitals")]
    [Authorize] // 🔹 Sensitive
    [ProducesResponseType(typeof(IEnumerable<PatientVitalResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetVitals(Guid id, CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetPatientVitalsQuery(id), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}/vitals/latest")]
    [Authorize] // 🔹 Sensitive
    [ProducesResponseType(typeof(PatientVitalResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetLatestVital(Guid id, CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetLatestVitalQuery(id), ct);
        if (result is null) return NoContent();
        return Ok(result);
    }

    // ══════════════════════════════════════════════════════
    //  POST / PUT / DELETE Endpoints
    // ══════════════════════════════════════════════════════

    [HttpPost]
    [Authorize] // 🔹 Sensitive
    [ProducesResponseType(typeof(PatientSummaryResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreatePatientRequestDto dto, CancellationToken ct = default)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await mediator.Send(new CreatePatientCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("{id:guid}/vitals")]
    [Authorize] // 🔹 Sensitive
    [ProducesResponseType(typeof(PatientVitalResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddVital(Guid id, [FromBody] PatientVitalRequestDto dto, CancellationToken ct = default)
    {
        dto.PatientId = id;
        var result = await mediator.Send(new AddPatientVitalCommand(dto), ct);
        return CreatedAtAction(nameof(GetLatestVital), new { id }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize] // 🔹 Sensitive
    [ProducesResponseType(typeof(PatientDetailResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePatientRequestDto dto, CancellationToken ct = default)
    {
        var result = await mediator.Send(new UpdatePatientCommand(id, dto), ct);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize] // 🔹 Sensitive
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
    {
        var success = await mediator.Send(new DeletePatientCommand(id), ct);
        return Ok(new { message = "পেশেন্ট সফলভাবে ডিলিট করা হয়েছে।", success });
    }
}

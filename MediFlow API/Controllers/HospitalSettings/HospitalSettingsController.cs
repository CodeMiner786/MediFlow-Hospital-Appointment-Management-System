using HealthcareHospitalManagement.Application.Commands.HospitalSettings;
using HealthcareHospitalManagement.Application.DTOs.HospitalSettings;
using HealthcareHospitalManagement.Application.Queries.HospitalSettings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.HospitalSettings
{
    [ApiController]
    [Route("api/hospital-settings")]
    public class HospitalSettingsController(IMediator mediator) : ControllerBase
    {
        // ── GET /api/hospital-settings/{id} ───────────────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetSettingsByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/hospital-settings/current ────────────────────────────────────
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent(CancellationToken ct)
        {
            var result = await mediator.Send(new GetCurrentSettingsQuery(), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── POST /api/hospital-settings ───────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateHospitalSettingsDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateHospitalSettingsCommand(dto), ct);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
                : BadRequest(result);
        }

        // ── PUT /api/hospital-settings/{id} ───────────────────────────────────────
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateHospitalSettingsDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateHospitalSettingsCommand(id, dto), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── PATCH /api/hospital-settings/shares?platformShare=20&doctorShare=80 ───
        [HttpPatch("shares")]
        public async Task<IActionResult> UpdateShares(
            [FromQuery] decimal platformShare,
            [FromQuery] decimal doctorShare,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new UpdateDefaultSharesCommand(platformShare, doctorShare), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── DELETE /api/hospital-settings/{id}/soft ───────────────────────────────
        [HttpDelete("{id:guid}/soft")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteHospitalSettingsCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/hospital-settings/{id}/restore ─────────────────────────────
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new RestoreHospitalSettingsCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/hospital-settings/{id} ────────────────────────────────────
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteHospitalSettingsCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

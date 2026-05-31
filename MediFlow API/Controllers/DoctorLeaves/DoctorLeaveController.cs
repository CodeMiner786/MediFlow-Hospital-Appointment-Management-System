using HealthcareHospitalManagement.Application.Commands.DoctorLeaves;
using HealthcareHospitalManagement.Application.DTOs.DoctorLeave;
using HealthcareHospitalManagement.Application.Queries.DoctorLeaves;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.DoctorLeaves
{
    [ApiController]
    [Route("api/doctor-leaves")]
    public class DoctorLeaveController(IMediator mediator) : ControllerBase
    {
        // ── GET /api/doctor-leaves/{id} ───────────────────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetLeaveByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-leaves/doctor/{doctorId}?pageNumber=1&pageSize=10 ─────
        [HttpGet("doctor/{doctorId:guid}")]
        public async Task<IActionResult> GetByDoctorId(
            Guid doctorId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetLeavesByDoctorIdQuery(doctorId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-leaves/pending?pageNumber=1&pageSize=10 ──────────────
        [HttpGet("pending")]
        public async Task<IActionResult> GetPending(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetPendingLeavesQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-leaves/date-range?start=&end= ────────────────────────
        [HttpGet("date-range")]
        public async Task<IActionResult> GetByDateRange(
            [FromQuery] DateTime start, [FromQuery] DateTime end,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetLeavesByDateRangeQuery(start, end, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-leaves/doctor/{doctorId}/on-leave?date= ──────────────
        [HttpGet("doctor/{doctorId:guid}/on-leave")]
        public async Task<IActionResult> IsDoctorOnLeave(
            Guid doctorId, [FromQuery] DateTime date, CancellationToken ct)
        {
            var result = await mediator.Send(new IsDoctorOnLeaveQuery(doctorId, date), ct);
            return Ok(result);
        }

        // ── POST /api/doctor-leaves ───────────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateDoctorLeaveDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateDoctorLeaveCommand(dto), ct);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
                : BadRequest(result);
        }

        // ── PUT /api/doctor-leaves/{id} ───────────────────────────────────────────
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateDoctorLeaveDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateDoctorLeaveCommand(id, dto), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── PATCH /api/doctor-leaves/{id}/approve ────────────────────────────────
        [HttpPatch("{id:guid}/approve")]
        public async Task<IActionResult> Approve(
            Guid id, [FromBody] ApproveDoctorLeaveDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new ApproveDoctorLeaveCommand(id, dto), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctor-leaves/{id}/soft ──────────────────────────────────
        [HttpDelete("{id:guid}/soft")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteDoctorLeaveCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/doctor-leaves/{id}/restore ────────────────────────────────
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new RestoreDoctorLeaveCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctor-leaves/{id} ───────────────────────────────────────
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteDoctorLeaveCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

using HealthcareHospitalManagement.Application.Commands.DoctorScheduleSlot;
using HealthcareHospitalManagement.Application.DTOs.DoctorScheduleSlot;
using HealthcareHospitalManagement.Application.Queries.DoctorScheduleSlot;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.DoctorScheduleSlot
{
    [ApiController]
    [Route("api/doctor-schedule-slots")]
    public class DoctorScheduleSlotController(IMediator mediator) : ControllerBase
    {
        // ── GET /api/doctor-schedule-slots/{id} ───────────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetSlotByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-schedule-slots/doctor/{doctorId}/date/{date} ──────────
        [HttpGet("doctor/{doctorId:guid}/date/{date}")]
        public async Task<IActionResult> GetByDoctorAndDate(
            Guid doctorId, DateTime date,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetSlotsByDoctorAndDateQuery(doctorId, date, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-schedule-slots/doctor/{doctorId}/available?date= ──────
        [HttpGet("doctor/{doctorId:guid}/available")]
        public async Task<IActionResult> GetAvailable(
            Guid doctorId, [FromQuery] DateTime date,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetAvailableSlotsQuery(doctorId, date, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-schedule-slots/schedule/{scheduleId} ──────────────────
        [HttpGet("schedule/{scheduleId:guid}")]
        public async Task<IActionResult> GetByScheduleId(
            Guid scheduleId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetSlotsByScheduleIdQuery(scheduleId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-schedule-slots/{id}/is-available ──────────────────────
        [HttpGet("{id:guid}/is-available")]
        public async Task<IActionResult> IsAvailable(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new IsSlotAvailableQuery(id), ct);
            return Ok(result);
        }

        // ── POST /api/doctor-schedule-slots ───────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateDoctorScheduleSlotDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateDoctorScheduleSlotCommand(dto), ct);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
                : BadRequest(result);
        }

        // ── PUT /api/doctor-schedule-slots/{id} ───────────────────────────────────
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateDoctorScheduleSlotDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateDoctorScheduleSlotCommand(id, dto), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── PATCH /api/doctor-schedule-slots/{id}/book ────────────────────────────
        [HttpPatch("{id:guid}/book")]
        public async Task<IActionResult> MarkAsBooked(
            Guid id, [FromQuery] Guid appointmentId, CancellationToken ct)
        {
            var result = await mediator.Send(new MarkSlotAsBookedCommand(id, appointmentId), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── PATCH /api/doctor-schedule-slots/{id}/block ───────────────────────────
        [HttpPatch("{id:guid}/block")]
        public async Task<IActionResult> MarkAsBlocked(
            Guid id, [FromQuery] string reason, CancellationToken ct)
        {
            var result = await mediator.Send(new MarkSlotAsBlockedCommand(id, reason), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── DELETE /api/doctor-schedule-slots/{id}/soft ───────────────────────────
        [HttpDelete("{id:guid}/soft")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteDoctorScheduleSlotCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/doctor-schedule-slots/{id}/restore ─────────────────────────
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new RestoreDoctorScheduleSlotCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctor-schedule-slots/{id} ────────────────────────────────
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteDoctorScheduleSlotCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

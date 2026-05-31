using HealthcareHospitalManagement.Application.Commands.StaffAttendance;
using HealthcareHospitalManagement.Application.DTOs.Staff.StaffAttendance;
using HealthcareHospitalManagement.Application.Queries.StaffAttendance;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.StaffAttendance
{
    [ApiController]
    [Route("api/staff-attendances")]
    public class StaffAttendanceController(IMediator mediator) : ControllerBase
    {
        // ── GET /api/staff-attendances/{id} ───────────────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetAttendanceByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/staff-attendances/staff/{staffId} ────────────────────────────
        [HttpGet("staff/{staffId:guid}")]
        public async Task<IActionResult> GetByStaff(
            Guid staffId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetAttendanceByStaffQuery(staffId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/staff-attendances/daily/{date} ───────────────────────────────
        [HttpGet("daily/{date}")]
        public async Task<IActionResult> GetDaily(
            DateOnly date,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetDailyAttendanceQuery(date, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/staff-attendances/staff/{staffId}/monthly?month=6&year=2025 ──
        [HttpGet("staff/{staffId:guid}/monthly")]
        public async Task<IActionResult> GetMonthly(
            Guid staffId,
            [FromQuery] int month, [FromQuery] int year,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetMonthlyAttendanceQuery(staffId, month, year, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/staff-attendances/staff/{staffId}/today ─────────────────────
        [HttpGet("staff/{staffId:guid}/today")]
        public async Task<IActionResult> GetToday(Guid staffId, CancellationToken ct)
        {
            var result = await mediator.Send(new GetTodayAttendanceQuery(staffId), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── POST /api/staff-attendances ───────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateStaffAttendanceDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateStaffAttendanceCommand(dto), ct);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
                : BadRequest(result);
        }

        // ── PUT /api/staff-attendances/{id} ───────────────────────────────────────
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateStaffAttendanceDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateStaffAttendanceCommand(id, dto), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/staff-attendances/staff/{staffId}/check-in?date=&checkInTime=
        [HttpPatch("staff/{staffId:guid}/check-in")]
        public async Task<IActionResult> CheckIn(
            Guid staffId,
            [FromQuery] DateOnly date,
            [FromQuery] TimeOnly checkInTime,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new CheckInCommand(staffId, date, checkInTime), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── PATCH /api/staff-attendances/staff/{staffId}/check-out?date=&checkOutTime=
        [HttpPatch("staff/{staffId:guid}/check-out")]
        public async Task<IActionResult> CheckOut(
            Guid staffId,
            [FromQuery] DateOnly date,
            [FromQuery] TimeOnly checkOutTime,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new CheckOutCommand(staffId, date, checkOutTime), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── DELETE /api/staff-attendances/{id}/soft ───────────────────────────────
        [HttpDelete("{id:guid}/soft")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteStaffAttendanceCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/staff-attendances/{id}/restore ─────────────────────────────
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new RestoreStaffAttendanceCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/staff-attendances/{id} ────────────────────────────────────
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteStaffAttendanceCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

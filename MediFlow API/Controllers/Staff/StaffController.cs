using HealthcareHospitalManagement.Application.Commands.Staff;
using HealthcareHospitalManagement.Application.DTOs.Staff;
using HealthcareHospitalManagement.Application.Queries.Staff;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Staff
{
    [ApiController]
    [Route("api/staff")]
    public class StaffController(IMediator mediator) : ControllerBase
    {
        // ── GET /api/staff/{id} ───────────────────────────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetStaffByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/staff/code/{staffCode} ───────────────────────────────────────
        [HttpGet("code/{staffCode}")]
        public async Task<IActionResult> GetByCode(string staffCode, CancellationToken ct)
        {
            var result = await mediator.Send(new GetStaffByCodeQuery(staffCode), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/staff/{id}/details ───────────────────────────────────────────
        [HttpGet("{id:guid}/details")]
        public async Task<IActionResult> GetWithDetails(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetStaffWithDetailsQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/staff?pageNumber=1&pageSize=10 ───────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetAllStaffQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/staff/type/{staffType} ───────────────────────────────────────
        [HttpGet("type/{staffType}")]
        public async Task<IActionResult> GetByType(
            StaffType staffType,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetStaffByTypeQuery(staffType, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/staff/department/{departmentId} ──────────────────────────────
        [HttpGet("department/{departmentId:guid}")]
        public async Task<IActionResult> GetByDepartment(
            Guid departmentId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetStaffByDepartmentQuery(departmentId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/staff/salary-range?minSalary=&maxSalary= ─────────────────────
        [HttpGet("salary-range")]
        public async Task<IActionResult> GetBySalaryRange(
            [FromQuery] decimal minSalary, [FromQuery] decimal maxSalary,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetStaffBySalaryRangeQuery(minSalary, maxSalary, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/staff/shift/{shiftType} ──────────────────────────────────────
        [HttpGet("shift/{shiftType}")]
        public async Task<IActionResult> GetByShift(
            ShiftType shiftType,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetStaffByShiftQuery(shiftType, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── POST /api/staff ───────────────────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateStaffDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateStaffCommand(dto), ct);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
                : BadRequest(result);
        }

        // ── PUT /api/staff/{id} ───────────────────────────────────────────────────
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateStaffDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateStaffCommand(id, dto), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/staff/{id}/soft ───────────────────────────────────────────
        [HttpDelete("{id:guid}/soft")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteStaffCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/staff/{id}/restore ─────────────────────────────────────────
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new RestoreStaffCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/staff/{id} ────────────────────────────────────────────────
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteStaffCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

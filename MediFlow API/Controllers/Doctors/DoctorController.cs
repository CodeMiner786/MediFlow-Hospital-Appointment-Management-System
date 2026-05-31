using HealthcareHospitalManagement.Application.Commands.Doctors;
using HealthcareHospitalManagement.Application.DTOs.Doctors;
using HealthcareHospitalManagement.Application.Queries.Doctors;
using HealthcareHospitalManagement.Domain.Enums.DoctorSpecialize;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Doctors
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController(IMediator mediator) : ControllerBase
    {
        // ── GET /api/doctors/{id} ─────────────────────────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetDoctorByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctors/code/{doctorCode} ────────────────────────────────────
        [HttpGet("code/{doctorCode}")]
        public async Task<IActionResult> GetByCode(string doctorCode, CancellationToken ct)
        {
            var result = await mediator.Send(new GetDoctorByCodeQuery(doctorCode), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctors/{id}/full-profile ────────────────────────────────────
        [HttpGet("{id:guid}/full-profile")]
        public async Task<IActionResult> GetFullProfile(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetDoctorFullProfileQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctors?pageNumber=1&pageSize=10 ─────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetAllDoctorsQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctors/specialization/{specialization} ─────────────────────
        [HttpGet("specialization/{specialization}")]
        public async Task<IActionResult> GetBySpecialization(
            DoctorSpecialization specialization,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetDoctorsBySpecializationQuery(specialization, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctors/department/{departmentId} ────────────────────────────
        [HttpGet("department/{departmentId:guid}")]
        public async Task<IActionResult> GetByDepartment(
            Guid departmentId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetDoctorsByDepartmentQuery(departmentId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctors/expiring-license?thresholdDate=2025-12-31 ───────────
        [HttpGet("expiring-license")]
        public async Task<IActionResult> GetExpiringLicense(
            [FromQuery] DateTime thresholdDate,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetDoctorsWithExpiringLicenseQuery(thresholdDate, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctors/top-rated?count=10 ──────────────────────────────────
        [HttpGet("top-rated")]
        public async Task<IActionResult> GetTopRated(
            [FromQuery] int count = 10, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetTopRatedDoctorsQuery(count), ct);
            return Ok(result);
        }

        // ── POST /api/doctors ─────────────────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateDoctorDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateDoctorCommand(dto), ct);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
                : BadRequest(result);
        }

        // ── PUT /api/doctors/{id} ─────────────────────────────────────────────────
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateDoctorDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateDoctorCommand(id, dto), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctors/{id} ──────────────────────────────────────────────
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteDoctorCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctors/{id}/soft ─────────────────────────────────────────
        [HttpDelete("{id:guid}/soft")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteDoctorCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/doctors/{id}/restore ──────────────────────────────────────
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new RestoreDoctorCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

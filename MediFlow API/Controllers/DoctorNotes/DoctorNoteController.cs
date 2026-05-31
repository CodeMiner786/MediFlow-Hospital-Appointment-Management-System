using HealthcareHospitalManagement.Application.Commands.DoctorNotes;
using HealthcareHospitalManagement.Application.DTOs.DoctorNote;
using HealthcareHospitalManagement.Application.Queries.DoctorNotes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.DoctorNotes
{
    [ApiController]
    [Route("api/doctor-notes")]
    public class DoctorNoteController(IMediator mediator) : ControllerBase
    {
        // ── GET /api/doctor-notes/{id} ────────────────────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetNoteByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-notes/patient/{patientId}?includePrivate=false ────────
        [HttpGet("patient/{patientId:guid}")]
        public async Task<IActionResult> GetByPatient(
            Guid patientId,
            [FromQuery] bool includePrivate = false,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetNotesByPatientQuery(patientId, includePrivate, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-notes/doctor/{doctorId} ───────────────────────────────
        [HttpGet("doctor/{doctorId:guid}")]
        public async Task<IActionResult> GetByDoctor(
            Guid doctorId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetNotesByDoctorQuery(doctorId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-notes/appointment/{appointmentId} ────────────────────
        [HttpGet("appointment/{appointmentId:guid}")]
        public async Task<IActionResult> GetByAppointment(Guid appointmentId, CancellationToken ct)
        {
            var result = await mediator.Send(new GetNoteByAppointmentQuery(appointmentId), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-notes/patient/{patientId}/pinned ─────────────────────
        [HttpGet("patient/{patientId:guid}/pinned")]
        public async Task<IActionResult> GetPinned(
            Guid patientId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetPinnedNotesByPatientQuery(patientId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-notes/patient/{patientId}/search?tag=Critical ─────────
        [HttpGet("patient/{patientId:guid}/search")]
        public async Task<IActionResult> SearchByTag(
            Guid patientId,
            [FromQuery] string tag,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new SearchNotesByTagQuery(patientId, tag, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── POST /api/doctor-notes ────────────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateDoctorNoteDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateDoctorNoteCommand(dto), ct);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
                : BadRequest(result);
        }

        // ── PUT /api/doctor-notes/{id} ────────────────────────────────────────────
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateDoctorNoteDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateDoctorNoteCommand(id, dto), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctor-notes/{id}/soft ───────────────────────────────────
        [HttpDelete("{id:guid}/soft")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteDoctorNoteCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/doctor-notes/{id}/restore ─────────────────────────────────
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new RestoreDoctorNoteCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctor-notes/{id} ────────────────────────────────────────
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteDoctorNoteCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

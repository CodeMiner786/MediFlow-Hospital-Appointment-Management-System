using HealthcareHospitalManagement.Application.Commands.DoctorNotifications;
using HealthcareHospitalManagement.Application.Queries.DoctorNotifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.DoctorNotifications
{
    [ApiController]
    [Route("api/doctor-notifications")]
    public class DoctorNotificationController(IMediator mediator) : ControllerBase
    {
        // ── GET /api/doctor-notifications/{id} ───────────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetNotificationByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-notifications/doctor/{doctorId} ───────────────────────
        [HttpGet("doctor/{doctorId:guid}")]
        public async Task<IActionResult> GetByDoctor(
            Guid doctorId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetNotificationsByDoctorQuery(doctorId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-notifications/doctor/{doctorId}/unread ───────────────
        [HttpGet("doctor/{doctorId:guid}/unread")]
        public async Task<IActionResult> GetUnread(
            Guid doctorId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetUnreadNotificationsQuery(doctorId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-notifications/doctor/{doctorId}/urgent ───────────────
        [HttpGet("doctor/{doctorId:guid}/urgent")]
        public async Task<IActionResult> GetUrgent(
            Guid doctorId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetUrgentNotificationsQuery(doctorId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-notifications/reference/{referenceId} ────────────────
        [HttpGet("reference/{referenceId:guid}")]
        public async Task<IActionResult> GetByReferenceId(
            Guid referenceId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetNotificationsByReferenceIdQuery(referenceId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── PATCH /api/doctor-notifications/{id}/mark-read ───────────────────────
        [HttpPatch("{id:guid}/mark-read")]
        public async Task<IActionResult> MarkAsRead(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new MarkNotificationAsReadCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/doctor-notifications/doctor/{doctorId}/mark-all-read ──────
        [HttpPatch("doctor/{doctorId:guid}/mark-all-read")]
        public async Task<IActionResult> MarkAllAsRead(Guid doctorId, CancellationToken ct)
        {
            var result = await mediator.Send(new MarkAllNotificationsAsReadCommand(doctorId), ct);
            return Ok(result);
        }

        // ── DELETE /api/doctor-notifications/{id}/soft ───────────────────────────
        [HttpDelete("{id:guid}/soft")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteDoctorNotificationCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/doctor-notifications/{id}/restore ─────────────────────────
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new RestoreDoctorNotificationCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctor-notifications/{id} ────────────────────────────────
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteDoctorNotificationCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

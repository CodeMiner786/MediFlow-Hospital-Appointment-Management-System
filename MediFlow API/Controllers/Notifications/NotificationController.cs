using HealthcareHospitalManagement.Application.Commands.Notifications;
using HealthcareHospitalManagement.Application.DTOs.Notification;
using HealthcareHospitalManagement.Application.Queries.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Notifications
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class NotificationController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // ──────────────────────────────────────────────────────────────────
        // POST  api/notification/send   🔒 Authorized
        // ──────────────────────────────────────────────────────────────────
        [Authorize]
        [HttpPost("send")]
        public async Task<IActionResult> SendNotification(
            [FromBody] SendNotificationRequestDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                new SendNotificationCommand(dto),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        // ──────────────────────────────────────────────────────────────────
        // PATCH  api/notification/{notificationId}/mark-read   🔒 Authorized
        // ──────────────────────────────────────────────────────────────────
        [Authorize]
        [HttpPatch("{notificationId:guid}/mark-read")]
        public async Task<IActionResult> MarkNotificationRead(
            [FromRoute] Guid notificationId,
            CancellationToken cancellationToken)
        {
            var dto = new MarkNotificationReadRequestDto { NotificationId = notificationId };
            var response = await _mediator.Send(
                new MarkNotificationReadCommand(dto),
                cancellationToken);

            if (!response.Success && response.Message?.Contains("not found") == true)
                return NotFound(response);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        // ──────────────────────────────────────────────────────────────────
        // GET  api/notification/user   🔒 Authorized
        // ──────────────────────────────────────────────────────────────────
        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserNotifications(
            [FromQuery] NotificationFilterRequestDto dto,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(
                new GetUserNotificationsQuery(dto),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        // ──────────────────────────────────────────────────────────────────
        // GET  api/notification/templates   🌐 Public
        // ──────────────────────────────────────────────────────────────────
        [AllowAnonymous]
        [HttpGet("templates")]
        public async Task<IActionResult> GetNotificationTemplates(
            [FromQuery] string? searchTerm,
            [FromQuery] string? channel,
            [FromQuery] string? category,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(
                new GetNotificationTemplatesQuery(searchTerm, channel, category, pageNumber, pageSize),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}

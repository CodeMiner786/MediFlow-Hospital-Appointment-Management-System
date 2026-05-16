using HealthcareHospitalManagement.Application.Commands.Chats;
using HealthcareHospitalManagement.Application.DTOs.Chat;
using HealthcareHospitalManagement.Application.Queries.Chats;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MediFlow_API.Controllers.Chats
{
    [Authorize] // সব endpoint এ authorize enforced
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ChatController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        // ── Helper: JWT থেকে current user Id নাও (null safe) ─────────────
        private Guid CurrentUserId
        {
            get
            {
                var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
            }
        }

        // ══════════════════════════════════════════════════════════════════
        // COMMANDS (Authorize required)
        // ══════════════════════════════════════════════════════════════════

        [HttpPost("conversations/start")]
        [Authorize] // logged-in user দরকার
        public async Task<IActionResult> StartConversation(
            [FromBody] StartConversationRequestDto dto,
            CancellationToken cancellationToken)
        {
            if (dto is null)
                return BadRequest("Request body cannot be null.");

            var response = await _mediator.Send(
                new StartConversationCommand(dto),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("messages/send")]
        [Authorize]
        public async Task<IActionResult> SendMessage(
            [FromBody] SendMessageRequestDto dto,
            CancellationToken cancellationToken)
        {
            if (dto is null)
                return BadRequest("Request body cannot be null.");

            var response = await _mediator.Send(
                new SendMessageCommand(dto),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPatch("messages/{messageId:guid}/read")]
        [Authorize]
        public async Task<IActionResult> MarkMessageRead(
            [FromRoute] Guid messageId,
            [FromQuery] Guid conversationId,
            CancellationToken cancellationToken)
        {
            if (CurrentUserId == Guid.Empty)
                return Unauthorized();

            var dto = new MarkMessageReadRequestDto
            {
                MessageId = messageId,
                ConversationId = conversationId,
                ReadByUserId = CurrentUserId
            };

            var response = await _mediator.Send(
                new MarkMessageReadCommand(dto),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        // ══════════════════════════════════════════════════════════════════
        // QUERIES (Authorize required)
        // ══════════════════════════════════════════════════════════════════

        [HttpGet("conversations")]
        [Authorize]
        public async Task<IActionResult> GetConversations(
            [FromQuery] bool? isActive,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            if (CurrentUserId == Guid.Empty)
                return Unauthorized();

            var dto = new ConversationFilterRequestDto
            {
                UserId = CurrentUserId,
                IsActive = isActive,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var response = await _mediator.Send(
                new GetConversationsQuery(dto),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("conversations/{conversationId:guid}/messages")]
        [Authorize]
        public async Task<IActionResult> GetMessages(
            [FromRoute] Guid conversationId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            if (CurrentUserId == Guid.Empty)
                return Unauthorized();

            var dto = new MessageFilterRequestDto
            {
                ConversationId = conversationId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var response = await _mediator.Send(
                new GetMessagesQuery(dto, CurrentUserId),
                cancellationToken);

            if (!response.Success &&
                 response.Message?.Contains("not found", StringComparison.OrdinalIgnoreCase) == true)
            {
                return NotFound(response);
            }

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("unread-count")]
        [Authorize]
        public async Task<IActionResult> GetUnreadCount(CancellationToken cancellationToken)
        {
            if (CurrentUserId == Guid.Empty)
                return Unauthorized();

            var response = await _mediator.Send(
                new GetUnreadCountQuery(CurrentUserId),
                cancellationToken);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}

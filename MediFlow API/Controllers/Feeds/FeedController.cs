using HealthcareHospitalManagement.Application.Commands.Feeds;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed;
using HealthcareHospitalManagement.Application.Queries.Feeds;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Feeds
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeedController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // ════════════════════════════════════════════════════════════════
        // QUERIES
        // ════════════════════════════════════════════════════════════════

        /// <summary>
        /// সব Feed Item পাবে — filter এবং pagination সহ
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<FeedItemResponseDto>>), 200)]
        public async Task<IActionResult> GetFeedItems([FromQuery] FeedFilterRequestDto filter, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetFeedItemsQuery(filter), cancellationToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// একটি নির্দিষ্ট Feed Item পাবে Id দিয়ে
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<FeedItemResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<FeedItemResponseDto>), 404)]
        public async Task<IActionResult> GetFeedItemById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetFeedItemByIdQuery(id), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }


        // ════════════════════════════════════════════════════════════════
        // COMMANDS
        // ════════════════════════════════════════════════════════════════

        /// <summary>
        /// নতুন Feed Item তৈরি করবে
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDto<FeedItemResponseDto>), 201)]
        [ProducesResponseType(typeof(ApiResponseDto<FeedItemResponseDto>), 400)]
        public async Task<IActionResult> CreateFeedItem([FromBody] CreateFeedItemRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateFeedItemCommand(dto), cancellationToken);
            return result.Success ? CreatedAtAction(nameof(GetFeedItemById), new { id = result.Data!.Id }, result) : BadRequest(result);
        }

        /// <summary>
        /// Feed Item আপডেট করবে
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseDto<FeedItemResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<FeedItemResponseDto>), 400)]
        [ProducesResponseType(typeof(ApiResponseDto<FeedItemResponseDto>), 404)]
        public async Task<IActionResult> UpdateFeedItem([FromBody] UpdateFeedItemRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateFeedItemCommand(dto), cancellationToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Feed Item Like / Unlike toggle করবে
        /// </summary>
        [HttpPost("like")]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), 404)]
        public async Task<IActionResult> LikeFeedItem([FromBody] FeedItemLikeRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new LikeFeedItemCommand(dto), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Feed Item Save / Unsave toggle করবে
        /// </summary>
        [HttpPost("save")]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), 404)]
        public async Task<IActionResult> SaveFeedItem([FromBody] FeedItemSaveRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new SaveFeedItemCommand(dto), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

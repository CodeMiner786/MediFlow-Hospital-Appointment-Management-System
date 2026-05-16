using HealthcareHospitalManagement.Application.Commands.Posts;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;
using HealthcareHospitalManagement.Application.Queries.Posts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Posts
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController(IMediator mediator) : ControllerBase
    {
        // ════════════════════════════════════════════════════════════════
        // QUERIES
        // ════════════════════════════════════════════════════════════════

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<PostResponseDto>>), 200)]
        public async Task<IActionResult> GetPosts([FromQuery] PostFilterRequestDto filter, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetPostsQuery(filter), cancellationToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<PostResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<PostResponseDto>), 404)]
        public async Task<IActionResult> GetPostById(Guid id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetPostByIdQuery(id), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("{postId:guid}/interactions")]
        [ProducesResponseType(typeof(ApiResponseDto<PostInteractionSummaryDto>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<PostInteractionSummaryDto>), 404)]
        public async Task<IActionResult> GetPostInteractions(Guid postId, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetPostInteractionsQuery(postId), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("{postId:guid}/comments")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<PostCommentResponseDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<PostCommentResponseDto>>), 404)]
        public async Task<IActionResult> GetPostComments(
            Guid postId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await mediator.Send(new GetPostCommentsQuery(postId, pageNumber, pageSize), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ════════════════════════════════════════════════════════════════
        // COMMANDS
        // ════════════════════════════════════════════════════════════════

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDto<PostResponseDto>), 201)]
        [ProducesResponseType(typeof(ApiResponseDto<PostResponseDto>), 400)]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new CreatePostCommand(dto), cancellationToken);
            return result.Success ? CreatedAtAction(nameof(GetPostById), new { id = result.Data!.Id }, result) : BadRequest(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseDto<PostResponseDto>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<PostResponseDto>), 400)]
        [ProducesResponseType(typeof(ApiResponseDto<PostResponseDto>), 404)]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new UpdatePostCommand(dto), cancellationToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("comment")]
        [ProducesResponseType(typeof(ApiResponseDto<PostCommentResponseDto>), 201)]
        [ProducesResponseType(typeof(ApiResponseDto<PostCommentResponseDto>), 404)]
        public async Task<IActionResult> AddPostComment([FromBody] AddPostCommentRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new AddPostCommentCommand(dto), cancellationToken);
            return result.Success ? Created(string.Empty, result) : BadRequest(result);
        }

        [HttpPost("like")]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), 404)]
        public async Task<IActionResult> LikePost([FromBody] PostLikeRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new LikePostCommand(dto), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost("save")]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), 404)]
        public async Task<IActionResult> SavePost([FromBody] PostSaveRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new SavePostCommand(dto), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost("share")]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), 404)]
        public async Task<IActionResult> SharePost([FromBody] PostShareRequestDto dto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new SharePostCommand(dto), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}

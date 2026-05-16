using HealthcareHospitalManagement.Application.Commands.Dashboards;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Dashboard;
using HealthcareHospitalManagement.Application.Queries.Dashboards;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Dashboards
{
    [ApiController]
    [Route("api/dashboard/lab")]
    [Authorize]
    public sealed class LabDashboardController(IMediator mediator) : ControllerBase
    {
        /// <summary>আজকের Lab ড্যাশবোর্ড মেট্রিক দেখুন।</summary>
        [HttpGet("today")]
        [Authorize(Roles = "Admin,LabManager")]
        [ProducesResponseType(typeof(ApiResponseDto<LabDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<LabDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLabDashboardToday(
            [FromQuery] Guid labProfileId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetLabDashboardTodayQuery(labProfileId), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>নির্দিষ্ট তারিখের Lab ড্যাশবোর্ড মেট্রিক দেখুন।</summary>
        [HttpGet("by-date")]
        [Authorize(Roles = "Admin,LabManager")]
        [ProducesResponseType(typeof(ApiResponseDto<LabDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<LabDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetLabDashboardByDate(
            [FromQuery] Guid labProfileId,
            [FromQuery] DateOnly date,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetLabDashboardByDateQuery(labProfileId, date), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>তারিখ রেঞ্জ অনুযায়ী Lab ড্যাশবোর্ড মেট্রিক (পেজিনেটেড)।</summary>
        [HttpGet("range")]
        [Authorize(Roles = "Admin,LabManager")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<LabDashboardResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLabDashboardRange(
            [FromQuery] Guid labProfileId,
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await mediator.Send(new GetLabDashboardRangeQuery(labProfileId, startDate, endDate, pageNumber, pageSize), cancellationToken);
            return Ok(result);
        }

        /// <summary>নতুন Lab ড্যাশবোর্ড মেট্রিক তৈরি করুন (Admin only)।</summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponseDto<LabDashboardResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponseDto<LabDashboardResponseDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateLabDashboard(
            [FromBody] CreateLabDashboardCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.Success ? CreatedAtAction(nameof(GetLabDashboardToday), result) : BadRequest(result);
        }

        /// <summary>Lab ড্যাশবোর্ড মেট্রিক আপডেট করুন।</summary>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,LabManager")]
        [ProducesResponseType(typeof(ApiResponseDto<LabDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<LabDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateLabDashboard(
            Guid id,
            [FromBody] UpdateLabDashboardCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest(ApiResponseDto<LabDashboardResponseDto>.Fail("Route Id এবং Body Id মিলছে না।"));

            var result = await mediator.Send(command, cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

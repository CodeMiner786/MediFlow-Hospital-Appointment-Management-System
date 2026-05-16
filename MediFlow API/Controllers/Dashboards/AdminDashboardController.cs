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
    [Route("api/dashboard/admin")]
    [Authorize(Roles = "Admin")]
    public sealed class AdminDashboardController(IMediator mediator) : ControllerBase
    {
        /// <summary>আজকের Admin ড্যাশবোর্ড মেট্রিক দেখুন।</summary>
        [HttpGet("today")]
        [ProducesResponseType(typeof(ApiResponseDto<AdminDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<AdminDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAdminDashboardToday(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAdminDashboardTodayQuery(), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>নির্দিষ্ট তারিখের Admin ড্যাশবোর্ড মেট্রিক দেখুন।</summary>
        [HttpGet("by-date")]
        [ProducesResponseType(typeof(ApiResponseDto<AdminDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<AdminDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAdminDashboardByDate(
            [FromQuery] DateOnly date,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAdminDashboardByDateQuery(date), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>তারিখ রেঞ্জ অনুযায়ী Admin ড্যাশবোর্ড মেট্রিক (পেজিনেটেড)।</summary>
        [HttpGet("range")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<AdminDashboardResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdminDashboardRange(
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await mediator.Send(new GetAdminDashboardRangeQuery(startDate, endDate, pageNumber, pageSize), cancellationToken);
            return Ok(result);
        }

        /// <summary>নতুন Admin ড্যাশবোর্ড মেট্রিক তৈরি করুন।</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDto<AdminDashboardResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponseDto<AdminDashboardResponseDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAdminDashboard(
            [FromBody] CreateAdminDashboardCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.Success ? CreatedAtAction(nameof(GetAdminDashboardToday), result) : BadRequest(result);
        }

        /// <summary>Admin ড্যাশবোর্ড মেট্রিক আপডেট করুন।</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<AdminDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<AdminDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAdminDashboard(
            Guid id,
            [FromBody] UpdateAdminDashboardCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest(ApiResponseDto<AdminDashboardResponseDto>.Fail("Route Id এবং Body Id মিলছে না।"));

            var result = await mediator.Send(command, cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

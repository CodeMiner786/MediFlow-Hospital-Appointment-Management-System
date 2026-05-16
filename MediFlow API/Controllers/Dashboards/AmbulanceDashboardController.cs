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
    [Route("api/dashboard/ambulance")]
    [Authorize]
    public sealed class AmbulanceDashboardController(IMediator mediator) : ControllerBase
    {
        /// <summary>আজকের Ambulance ড্যাশবোর্ড মেট্রিক দেখুন।</summary>
        [HttpGet("today")]
        [Authorize(Roles = "Admin,AmbulanceProvider")]
        [ProducesResponseType(typeof(ApiResponseDto<AmbulanceDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<AmbulanceDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAmbulanceDashboardToday(
            [FromQuery] Guid providerId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAmbulanceDashboardTodayQuery(providerId), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>নির্দিষ্ট তারিখের Ambulance ড্যাশবোর্ড মেট্রিক দেখুন।</summary>
        [HttpGet("by-date")]
        [Authorize(Roles = "Admin,AmbulanceProvider")]
        [ProducesResponseType(typeof(ApiResponseDto<AmbulanceDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<AmbulanceDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAmbulanceDashboardByDate(
            [FromQuery] Guid providerId,
            [FromQuery] DateOnly date,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAmbulanceDashboardByDateQuery(providerId, date), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>তারিখ রেঞ্জ অনুযায়ী Ambulance ড্যাশবোর্ড মেট্রিক (পেজিনেটেড)।</summary>
        [HttpGet("range")]
        [Authorize(Roles = "Admin,AmbulanceProvider")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<AmbulanceDashboardResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAmbulanceDashboardRange(
            [FromQuery] Guid providerId,
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await mediator.Send(new GetAmbulanceDashboardRangeQuery(providerId, startDate, endDate, pageNumber, pageSize), cancellationToken);
            return Ok(result);
        }

        /// <summary>নতুন Ambulance ড্যাশবোর্ড মেট্রিক তৈরি করুন (Admin only)।</summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponseDto<AmbulanceDashboardResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponseDto<AmbulanceDashboardResponseDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAmbulanceDashboard(
            [FromBody] CreateAmbulanceDashboardCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.Success ? CreatedAtAction(nameof(GetAmbulanceDashboardToday), result) : BadRequest(result);
        }

        /// <summary>Ambulance ড্যাশবোর্ড মেট্রিক আপডেট করুন।</summary>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,AmbulanceProvider")]
        [ProducesResponseType(typeof(ApiResponseDto<AmbulanceDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<AmbulanceDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAmbulanceDashboard(
            Guid id,
            [FromBody] UpdateAmbulanceDashboardCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest(ApiResponseDto<AmbulanceDashboardResponseDto>.Fail("Route Id এবং Body Id মিলছে না।"));

            var result = await mediator.Send(command, cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

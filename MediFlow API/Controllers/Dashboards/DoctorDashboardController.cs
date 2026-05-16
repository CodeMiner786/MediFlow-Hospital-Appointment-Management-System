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
    [Route("api/dashboard/doctor")]
    [Authorize]
    public sealed class DoctorDashboardController(IMediator mediator) : ControllerBase
    {
        /// <summary>আজকের Doctor ড্যাশবোর্ড মেট্রিক দেখুন।</summary>
        [HttpGet("today")]
        [Authorize(Roles = "Admin,Doctor")]
        [ProducesResponseType(typeof(ApiResponseDto<DoctorDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<DoctorDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDoctorDashboardToday(
            [FromQuery] Guid doctorId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetDoctorDashboardTodayQuery(doctorId), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>নির্দিষ্ট তারিখের Doctor ড্যাশবোর্ড মেট্রিক দেখুন।</summary>
        [HttpGet("by-date")]
        [Authorize(Roles = "Admin,Doctor")]
        [ProducesResponseType(typeof(ApiResponseDto<DoctorDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<DoctorDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDoctorDashboardByDate(
            [FromQuery] Guid doctorId,
            [FromQuery] DateOnly date,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetDoctorDashboardByDateQuery(doctorId, date), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>তারিখ রেঞ্জ অনুযায়ী Doctor ড্যাশবোর্ড মেট্রিক (পেজিনেটেড)।</summary>
        [HttpGet("range")]
        [Authorize(Roles = "Admin,Doctor")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<DoctorDashboardResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDoctorDashboardRange(
            [FromQuery] Guid doctorId,
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await mediator.Send(new GetDoctorDashboardRangeQuery(doctorId, startDate, endDate, pageNumber, pageSize), cancellationToken);
            return Ok(result);
        }

        /// <summary>নতুন Doctor ড্যাশবোর্ড মেট্রিক তৈরি করুন (Admin only)।</summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponseDto<DoctorDashboardResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponseDto<DoctorDashboardResponseDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDoctorDashboard(
            [FromBody] CreateDoctorDashboardCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.Success ? CreatedAtAction(nameof(GetDoctorDashboardToday), result) : BadRequest(result);
        }

        /// <summary>Doctor ড্যাশবোর্ড মেট্রিক আপডেট করুন।</summary>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Doctor")]
        [ProducesResponseType(typeof(ApiResponseDto<DoctorDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<DoctorDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDoctorDashboard(
            Guid id,
            [FromBody] UpdateDoctorDashboardCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest(ApiResponseDto<DoctorDashboardResponseDto>.Fail("Route Id এবং Body Id মিলছে না।"));

            var result = await mediator.Send(command, cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

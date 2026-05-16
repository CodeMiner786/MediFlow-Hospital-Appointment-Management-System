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
    [Route("api/dashboard/pharmacy")]
    [Authorize]
    public sealed class PharmacyDashboardController(IMediator mediator) : ControllerBase
    {
        /// <summary>আজকের Pharmacy ড্যাশবোর্ড মেট্রিক দেখুন।</summary>
        [HttpGet("today")]
        [Authorize(Roles = "Admin,Pharmacist")]
        [ProducesResponseType(typeof(ApiResponseDto<PharmacyDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<PharmacyDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPharmacyDashboardToday(
            [FromQuery] Guid pharmacyProfileId,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetPharmacyDashboardTodayQuery(pharmacyProfileId), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>নির্দিষ্ট তারিখের Pharmacy ড্যাশবোর্ড মেট্রিক দেখুন।</summary>
        [HttpGet("by-date")]
        [Authorize(Roles = "Admin,Pharmacist")]
        [ProducesResponseType(typeof(ApiResponseDto<PharmacyDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<PharmacyDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPharmacyDashboardByDate(
            [FromQuery] Guid pharmacyProfileId,
            [FromQuery] DateOnly date,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetPharmacyDashboardByDateQuery(pharmacyProfileId, date), cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>তারিখ রেঞ্জ অনুযায়ী Pharmacy ড্যাশবোর্ড মেট্রিক (পেজিনেটেড)।</summary>
        [HttpGet("range")]
        [Authorize(Roles = "Admin,Pharmacist")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<PharmacyDashboardResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPharmacyDashboardRange(
            [FromQuery] Guid pharmacyProfileId,
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var result = await mediator.Send(new GetPharmacyDashboardRangeQuery(pharmacyProfileId, startDate, endDate, pageNumber, pageSize), cancellationToken);
            return Ok(result);
        }

        /// <summary>নতুন Pharmacy ড্যাশবোর্ড মেট্রিক তৈরি করুন (Admin only)।</summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponseDto<PharmacyDashboardResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponseDto<PharmacyDashboardResponseDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePharmacyDashboard(
            [FromBody] CreatePharmacyDashboardCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.Success ? CreatedAtAction(nameof(GetPharmacyDashboardToday), result) : BadRequest(result);
        }

        /// <summary>Pharmacy ড্যাশবোর্ড মেট্রিক আপডেট করুন।</summary>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Pharmacist")]
        [ProducesResponseType(typeof(ApiResponseDto<PharmacyDashboardResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseDto<PharmacyDashboardResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePharmacyDashboard(
            Guid id,
            [FromBody] UpdatePharmacyDashboardCommand command,
            CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest(ApiResponseDto<PharmacyDashboardResponseDto>.Fail("Route Id এবং Body Id মিলছে না।"));

            var result = await mediator.Send(command, cancellationToken);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

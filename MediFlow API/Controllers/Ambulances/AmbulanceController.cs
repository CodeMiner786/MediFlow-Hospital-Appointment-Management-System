using HealthcareHospitalManagement.Application.Commands.Ambulances;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Ambulance;
using HealthcareHospitalManagement.Application.Queries.Ambulances;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Ambulances
{
    [ApiController]
    [Route("api/ambulance")]
    public class AmbulanceController(IMediator mediator) : ControllerBase
    {
        // ══════════════════════════════════════════════════════
        //  BOOKING ENDPOINTS
        // ══════════════════════════════════════════════════════

        /// GET /api/ambulance/bookings?pageNumber=1&pageSize=10
        [HttpGet("bookings")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<AmbulanceBookingResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBookings(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetAllAmbulanceBookingsQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        /// GET /api/ambulance/bookings/active?pageNumber=1&pageSize=10
        [HttpGet("bookings/active")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<AmbulanceBookingResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveBookings(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetActiveBookingsQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        /// GET /api/ambulance/bookings/{id}
        [HttpGet("bookings/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<AmbulanceBookingResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBookingById(
            Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetAmbulanceBookingByIdQuery(id), ct);
            return Ok(result);
        }

        /// POST /api/ambulance/bookings
        [HttpPost("bookings")]
        [ProducesResponseType(typeof(ApiResponseDto<AmbulanceBookingResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BookAmbulance(
            [FromBody] BookAmbulanceRequestDto dto,
            CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await mediator.Send(new BookAmbulanceCommand(dto), ct);
            return Ok(result);
        }

        /// PUT /api/ambulance/bookings/status
        [HttpPut("bookings/status")]
        [ProducesResponseType(typeof(ApiResponseDto<AmbulanceBookingResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBookingStatus(
            [FromBody] UpdateAmbulanceBookingStatusRequestDto dto,
            CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await mediator.Send(new UpdateAmbulanceBookingStatusCommand(dto), ct);
            return Ok(result);
        }

        /// DELETE /api/ambulance/bookings/{id}/cancel?reason=...
        [HttpDelete("bookings/{id:guid}/cancel")]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CancelBooking(
            Guid id,
            [FromQuery] string? reason = null,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new CancelAmbulanceBookingCommand(id, reason), ct);
            return Ok(result);
        }

        // ══════════════════════════════════════════════════════
        //  PROVIDER ENDPOINTS
        // ══════════════════════════════════════════════════════

        /// GET /api/ambulance/providers/{id}
        [HttpGet("providers/{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<AmbulanceProviderResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProviderById(
            Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetAmbulanceProviderByIdQuery(id), ct);
            return Ok(result);
        }

        /// GET /api/ambulance/providers/search?city=Dhaka&pageNumber=1&pageSize=10
        [HttpGet("providers/search")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<AmbulanceProviderResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchProviders(
            [FromQuery] AmbulanceSearchRequestDto dto,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new SearchAmbulanceProvidersQuery(dto), ct);
            return Ok(result);
        }

        // ══════════════════════════════════════════════════════
        //  VEHICLE ENDPOINTS
        // ══════════════════════════════════════════════════════

        /// GET /api/ambulance/providers/{providerId}/vehicles?pageNumber=1&pageSize=10
        [HttpGet("providers/{providerId:guid}/vehicles")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<AmbulanceVehicleResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVehiclesByProvider(
            Guid providerId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetVehiclesByProviderQuery(providerId, pageNumber, pageSize), ct);
            return Ok(result);
        }
    }

}

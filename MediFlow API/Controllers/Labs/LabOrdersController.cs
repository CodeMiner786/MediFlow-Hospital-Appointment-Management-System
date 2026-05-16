using HealthcareHospitalManagement.Application.Commands.Labs;
using HealthcareHospitalManagement.Application.DTOs.Lab;
using HealthcareHospitalManagement.Application.Queries.Labs;
using HealthcareHospitalManagement.Domain.Common.ApiResponse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Labs
{
    [ApiController]
    [Route("api/[controller]")]
    public class LabOrdersController(ISender mediator) : ControllerBase
    {
        // 1. Create Lab Order
        [Authorize] // 🔹 Sensitive
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), 200)]
        public async Task<ActionResult<ApiResponse<Guid>>> CreateLabOrder(
            [FromBody] CreateLabOrderRequestDto dto)
        {
            var result = await mediator.Send(new CreateLabOrderCommand(dto));
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        // 2. Filter Lab Orders
        [AllowAnonymous] // 🔹 Public
        [HttpPost("filter")]
        [ProducesResponseType(typeof(ApiResponse<List<LabOrderResponseDto>>), 200)]
        public async Task<ActionResult<ApiResponse<List<LabOrderResponseDto>>>> FilterLabOrders(
            [FromBody] LabOrderFilterRequestDto dto)
        {
            var result = await mediator.Send(new GetLabOrdersQuery(dto));
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        // 3. Single Lab Order Detail
        [Authorize] // 🔹 Sensitive
        [HttpGet("{labOrderId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<LabOrderResponseDto>), 200)]
        public async Task<IActionResult> GetLabOrderDetail(Guid labOrderId)
        {
            var result = await mediator.Send(new GetLabOrderDetailQuery(labOrderId));
            return Ok(result);
        }

        // 4. Patient এর সব Lab Orders
        [Authorize] // 🔹 Sensitive
        [HttpGet("patient/{patientId:guid}")]
        [ProducesResponseType(typeof(ApiResponse<List<LabOrderResponseDto>>), 200)]
        public async Task<IActionResult> GetPatientLabOrders(Guid patientId)
        {
            var result = await mediator.Send(new GetPatientLabOrdersQuery(patientId));
            return Ok(result);
        }

        // 5. Order এ Item Add
        [Authorize] // 🔹 Sensitive
        [HttpPost("{labOrderId:guid}/item/add")]
        [ProducesResponseType(typeof(ApiResponse<Guid>), 200)]
        public async Task<IActionResult> AddLabOrderItem(Guid labOrderId,
            [FromBody] LabOrderItemRequestDto dto)
        {
            var result = await mediator.Send(new AddLabOrderItemCommand(labOrderId, dto));
            return Ok(result);
        }

        // 6. Order এর সব Items
        [Authorize] // 🔹 Sensitive
        [HttpGet("{labOrderId:guid}/items")]
        [ProducesResponseType(typeof(ApiResponse<List<LabOrderItemResponseDto>>), 200)]
        public async Task<IActionResult> GetLabOrderItems(Guid labOrderId)
        {
            var result = await mediator.Send(new GetLabOrderItemsQuery(labOrderId));
            return Ok(result);
        }

        // 7. সব Verified Labs
        [AllowAnonymous] // 🔹 Public
        [HttpGet("profile/verified")]
        [ProducesResponseType(typeof(ApiResponse<List<LabProfileResponseDto>>), 200)]
        public async Task<IActionResult> GetVerifiedLabs()
        {
            var result = await mediator.Send(new GetVerifiedLabsQuery());
            return Ok(result);
        }

        // 8. City অনুযায়ী Labs
        [AllowAnonymous] // 🔹 Public
        [HttpGet("profile/city/{city}")]
        [ProducesResponseType(typeof(ApiResponse<List<LabProfileResponseDto>>), 200)]
        public async Task<IActionResult> GetLabsByCity(string city)
        {
            var result = await mediator.Send(new GetLabsByCityQuery(city));
            return Ok(result);
        }

        // 9. Category অনুযায়ী Tests
        [AllowAnonymous] // 🔹 Public
        [HttpGet("test/category/{category}")]
        [ProducesResponseType(typeof(ApiResponse<List<LabTestResponseDto>>), 200)]
        public async Task<IActionResult> GetLabTestsByCategory(string category)
        {
            var result = await mediator.Send(new GetLabTestsByCategoryQuery(category));
            return Ok(result);
        }

        // 10. Test Name দিয়ে Search
        [AllowAnonymous] // 🔹 Public
        [HttpGet("test/search")]
        [ProducesResponseType(typeof(ApiResponse<List<LabTestResponseDto>>), 200)]
        public async Task<IActionResult> SearchLabTests(
            [FromQuery] string searchTerm,
            [FromQuery] Guid labProfileId)
        {
            var result = await mediator.Send(new SearchLabTestsQuery(searchTerm, labProfileId));
            return Ok(result);
        }
    }
}

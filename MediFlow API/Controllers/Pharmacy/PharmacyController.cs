using HealthcareHospitalManagement.Application.Commands.Pharmacys;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Pharmacy;
using HealthcareHospitalManagement.Application.Queries.Pharmacys;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.Pharmacy
{
    [ApiController]
    [Route("api/pharmacy")]
    public class PharmacyController(IMediator mediator) : ControllerBase
    {
        // ══════════════════════════════════════════════════════
        //  MEDICINE ENDPOINTS
        // ══════════════════════════════════════════════════════

        [HttpGet("medicines/{id:guid}")]
        [AllowAnonymous] // 🔹 Public
        [ProducesResponseType(typeof(ApiResponseDto<MedicineResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMedicineById(Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetMedicineByIdQuery(id), ct);
            return Ok(result);
        }

        [HttpGet("medicines/search")]
        [AllowAnonymous] // 🔹 Public
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<MedicineResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchMedicines([FromQuery] MedicineSearchRequestDto dto, CancellationToken ct = default)
        {
            var result = await mediator.Send(new SearchMedicinesQuery(dto), ct);
            return Ok(result);
        }

        [HttpGet("{pharmacyId:guid}/medicines")]
        [AllowAnonymous] // 🔹 Public
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<MedicineResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMedicinesByPharmacy(Guid pharmacyId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetMedicinesByPharmacyQuery(pharmacyId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        [HttpPost("medicines")]
        [Authorize] // 🔹 Sensitive
        [ProducesResponseType(typeof(ApiResponseDto<MedicineResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMedicine([FromBody] CreateMedicineRequestDto dto, CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await mediator.Send(new CreateMedicineCommand(dto), ct);
            return Ok(result);
        }

        [HttpDelete("medicines/{id:guid}")]
        [Authorize] // 🔹 Sensitive
        [ProducesResponseType(typeof(ApiResponseDto<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteMedicine(Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new DeleteMedicineCommand(id), ct);
            return Ok(result);
        }

        // ══════════════════════════════════════════════════════
        //  PRESCRIPTION ENDPOINTS
        // ══════════════════════════════════════════════════════

        [HttpGet("prescriptions/{id:guid}")]
        [Authorize] // 🔹 Sensitive
        [ProducesResponseType(typeof(ApiResponseDto<PrescriptionResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPrescriptionById(Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetPrescriptionByIdQuery(id), ct);
            return Ok(result);
        }

        [HttpGet("prescriptions/patient/{patientId:guid}")]
        [Authorize] // 🔹 Sensitive
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<PrescriptionResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPrescriptionsByPatient(Guid patientId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetPrescriptionsByPatientQuery(patientId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        [HttpPost("prescriptions")]
        [Authorize] // 🔹 Sensitive
        [ProducesResponseType(typeof(ApiResponseDto<PrescriptionResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionRequestDto dto, CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await mediator.Send(new CreatePrescriptionCommand(dto), ct);
            return Ok(result);
        }

        // ══════════════════════════════════════════════════════
        //  MEDICINE ORDER ENDPOINTS
        // ══════════════════════════════════════════════════════

        [HttpGet("orders/{id:guid}")]
        [Authorize] // 🔹 Sensitive
        [ProducesResponseType(typeof(ApiResponseDto<MedicineOrderResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrderById(Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetMedicineOrderByIdQuery(id), ct);
            return Ok(result);
        }

        [HttpGet("orders/patient/{patientId:guid}")]
        [Authorize] // 🔹 Sensitive
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<MedicineOrderResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrdersByPatient(Guid patientId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetOrdersByPatientQuery(patientId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        [HttpGet("orders/today")]
        [Authorize] // 🔹 Sensitive
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<MedicineOrderResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodaysOrders([FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetTodaysOrdersQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        [HttpPost("orders")]
        [Authorize] // 🔹 Sensitive
        [ProducesResponseType(typeof(ApiResponseDto<MedicineOrderResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PlaceOrder([FromBody] MedicineOrderRequestDto dto, CancellationToken ct = default)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await mediator.Send(new PlaceMedicineOrderCommand(dto), ct);
            return Ok(result);
        }

        [HttpDelete("orders/{id:guid}/cancel")]
        [Authorize] // 🔹 Sensitive
        [ProducesResponseType(typeof(ApiResponseDto<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CancelOrder(Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new CancelMedicineOrderCommand(id), ct);
            return Ok(result);
        }

        // ══════════════════════════════════════════════════════
        //  PHARMACY PROFILE ENDPOINT
        // ══════════════════════════════════════════════════════

        [HttpGet("{id:guid}/profile")]
        [AllowAnonymous] // 🔹 Public
        [ProducesResponseType(typeof(ApiResponseDto<PharmacyProfileResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPharmacyProfile(Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetPharmacyProfileQuery(id), ct);
            return Ok(result);
        }
    }
}

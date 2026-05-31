using HealthcareHospitalManagement.Application.Commands.DoctorDocuments;
using HealthcareHospitalManagement.Application.DTOs.DoctorDocument;
using HealthcareHospitalManagement.Application.Queries.DoctorDocuments;
using HealthcareHospitalManagement.Domain.Enums.DoctorDocument;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.DoctorDocuments
{
    [ApiController]
    [Route("api/doctor-documents")]
    [Authorize]
    public class DoctorDocumentController(IMediator mediator) : ControllerBase
    {
        // ════════════════════════════════════════════════════════════════
        // QUERIES
        // ════════════════════════════════════════════════════════════════

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetDocumentByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("by-doctor/{doctorId:guid}")]
        public async Task<IActionResult> GetByDoctorId(Guid doctorId, CancellationToken ct)
        {
            var result = await mediator.Send(new GetDocumentsByDoctorIdQuery(doctorId), ct);
            return Ok(result);
        }

        [HttpGet("by-verification")]
        public async Task<IActionResult> GetByVerificationStatus(
            [FromQuery] bool isVerified, CancellationToken ct)
        {
            var result = await mediator.Send(new GetDocumentsByVerificationStatusQuery(isVerified), ct);
            return Ok(result);
        }

        [HttpGet("by-type")]
        public async Task<IActionResult> GetByType(
            [FromQuery] DoctorDocumentType documentType, CancellationToken ct)
        {
            var result = await mediator.Send(new GetDocumentsByTypeQuery(documentType), ct);
            return Ok(result);
        }

        [HttpGet("expiring")]
        public async Task<IActionResult> GetExpiring(
            [FromQuery] DateTime thresholdDate, CancellationToken ct)
        {
            var result = await mediator.Send(new GetExpiringDocumentsQuery(thresholdDate), ct);
            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetPagedDocumentsQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ════════════════════════════════════════════════════════════════
        // COMMANDS
        // ════════════════════════════════════════════════════════════════

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateDoctorDocumentDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateDoctorDocumentCommand(dto), ct);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
                : BadRequest(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateDoctorDocumentDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateDoctorDocumentCommand(id, dto), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPatch("{id:guid}/verify")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Verify(
            Guid id, [FromBody] VerifyDoctorDocumentDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new VerifyDoctorDocumentCommand(id, dto), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id:guid}/soft")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteDoctorDocumentCommand(id), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteDoctorDocumentCommand(id), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }

}

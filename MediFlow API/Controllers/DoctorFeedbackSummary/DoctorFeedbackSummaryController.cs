using HealthcareHospitalManagement.Application.Commands.DoctorFeedbackSummary;
using HealthcareHospitalManagement.Application.DTOs.DoctorFeedbackSummary;
using HealthcareHospitalManagement.Application.Queries.DoctorFeedbackSummary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.DoctorFeedbackSummary
{
    [ApiController]
    [Route("api/doctor-feedback-summaries")]
    public class DoctorFeedbackSummaryController(IMediator mediator) : ControllerBase
    {
        // ── GET /api/doctor-feedback-summaries/{id} ───────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetFeedbackSummaryByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-feedback-summaries/doctor/{doctorId} ─────────────────
        [HttpGet("doctor/{doctorId:guid}")]
        public async Task<IActionResult> GetByDoctorId(Guid doctorId, CancellationToken ct)
        {
            var result = await mediator.Send(new GetFeedbackSummaryByDoctorIdQuery(doctorId), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-feedback-summaries/high-rated?minRating=4&pageNumber=1 
        [HttpGet("high-rated")]
        public async Task<IActionResult> GetHighRated(
            [FromQuery] decimal minRating,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetHighRatedSummariesQuery(minRating, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-feedback-summaries?pageNumber=1&pageSize=10 ───────────
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetAllFeedbackSummariesQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── POST /api/doctor-feedback-summaries ───────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateDoctorFeedbackSummaryDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateDoctorFeedbackSummaryCommand(dto), ct);
            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
                : BadRequest(result);
        }

        // ── PUT /api/doctor-feedback-summaries/{id} ───────────────────────────────
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateDoctorFeedbackSummaryDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateDoctorFeedbackSummaryCommand(id, dto), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/doctor-feedback-summaries/doctor/{doctorId}/stats ──────────
        [HttpPatch("doctor/{doctorId:guid}/stats")]
        public async Task<IActionResult> UpdateStats(
            Guid doctorId,
            [FromQuery] decimal newAverage,
            [FromQuery] int totalReviews,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new UpdateSummaryStatsByDoctorCommand(doctorId, newAverage, totalReviews), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctor-feedback-summaries/{id}/soft ──────────────────────
        [HttpDelete("{id:guid}/soft")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteDoctorFeedbackSummaryCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/doctor-feedback-summaries/{id}/restore ────────────────────
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new RestoreDoctorFeedbackSummaryCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE /api/doctor-feedback-summaries/{id} ───────────────────────────
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteDoctorFeedbackSummaryCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}

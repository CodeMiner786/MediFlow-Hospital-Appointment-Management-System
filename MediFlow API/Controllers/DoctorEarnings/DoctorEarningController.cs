using HealthcareHospitalManagement.Application.Commands.DoctorEarnings;
using HealthcareHospitalManagement.Application.DTOs.DoctorEarning;
using HealthcareHospitalManagement.Application.Queries.DoctorEarnings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediFlow_API.Controllers.DoctorEarnings
{
    [ApiController]
    [Route("api/doctor-earnings")]
    public class DoctorEarningController(IMediator mediator) : ControllerBase
    {
        // ── GET /api/doctor-earnings/{id} ────────────────────────────────────────
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new GetEarningByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-earnings/doctor/{doctorId}?pageNumber=1&pageSize=10 ──
        [HttpGet("doctor/{doctorId:guid}")]
        public async Task<IActionResult> GetByDoctor(
            Guid doctorId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetEarningsByDoctorQuery(doctorId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-earnings/doctor/{doctorId}/date-range ─────────────────
        [HttpGet("doctor/{doctorId:guid}/date-range")]
        public async Task<IActionResult> GetByDateRange(
            Guid doctorId,
            [FromQuery] DateTime start,
            [FromQuery] DateTime end,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetEarningsByDateRangeQuery(doctorId, start, end, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-earnings/doctor/{doctorId}/unpaid ────────────────────
        [HttpGet("doctor/{doctorId:guid}/unpaid")]
        public async Task<IActionResult> GetUnpaid(
            Guid doctorId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetUnpaidEarningsQuery(doctorId, pageNumber, pageSize), ct);
            return Ok(result);
        }

        // ── GET /api/doctor-earnings/appointment/{appointmentId} ─────────────────
        [HttpGet("appointment/{appointmentId:guid}")]
        public async Task<IActionResult> GetByAppointment(Guid appointmentId, CancellationToken ct)
        {
            var result = await mediator.Send(new GetEarningByAppointmentQuery(appointmentId), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET /api/doctor-earnings/doctor/{doctorId}/total?onlyPaid=true ───────
        [HttpGet("doctor/{doctorId:guid}/total")]
        public async Task<IActionResult> GetTotalShare(
            Guid doctorId, [FromQuery] bool onlyPaid = false, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetDoctorTotalShareQuery(doctorId, onlyPaid), ct);
            return Ok(result);
        }

        // ── POST /api/doctor-earnings ─────────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateDoctorEarningDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new CreateDoctorEarningCommand(dto), ct);
            return result.Success ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result)
                                  : BadRequest(result);
        }

        // ── PUT /api/doctor-earnings/{id} ────────────────────────────────────────
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id, [FromBody] UpdateDoctorEarningDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new UpdateDoctorEarningCommand(id, dto), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/doctor-earnings/{id}/mark-paid ─────────────────────────────
        [HttpPatch("{id:guid}/mark-paid")]
        public async Task<IActionResult> MarkPaid(
            Guid id, [FromBody] MarkEarningPaidDto dto, CancellationToken ct)
        {
            var result = await mediator.Send(new MarkEarningPaidCommand(id, dto), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── DELETE /api/doctor-earnings/{id} ──────────────────────────────────────
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new DeleteDoctorEarningCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }


        // ── DELETE /api/doctor-earnings/{id}/soft ─────────────────────────────────
        [HttpDelete("{id:guid}/soft")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new SoftDeleteDoctorEarningCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── PATCH /api/doctor-earnings/{id}/restore ───────────────────────────────
        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            var result = await mediator.Send(new RestoreDoctorEarningCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

    }

}

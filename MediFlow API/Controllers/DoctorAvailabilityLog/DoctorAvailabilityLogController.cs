using HealthcareHospitalManagement.Application.Commands.DoctorAvailabilityLog;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.DoctorAvailabilityLog;
using HealthcareHospitalManagement.Application.Queries.DoctorAvailablityLog;
using HealthcareHospitalManagement.Domain.Enums.DoctorStaff;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediFlow_API.Controllers.DoctorAvailabilityLog
{
    [ApiController]
    [Route("api/[controller]")]
    // ক্লাসের নামের সাথেই সরাসরি প্রাইমারি কনস্ট্রাক্টরের মাধ্যমে IMediator ইনজেক্ট করা হয়েছে
    public class DoctorAvailabilityLogController(IMediator mediator) : ControllerBase
    {
        // ── GET: api/DoctorAvailabilityLog?pageNumber=1&pageSize=10 ──────────────
        /// <summary>সব লগ পেজিনেশন সহ আনে</summary>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<DoctorAvailabilityLogDto>>), 200)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            // আলাদা ফিল্ড ছাড়া সরাসরি 'mediator' ব্যবহার করা হয়েছে
            var result = await mediator.Send(
                new GetAllDoctorAvailabilityLogsQuery(pageNumber, pageSize), ct);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── GET: api/DoctorAvailabilityLog/{id} ──────────────────────────────────
        /// <summary>নির্দিষ্ট আইডির লগ আনে</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<DoctorAvailabilityLogDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetDoctorAvailabilityLogByIdQuery(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET: api/DoctorAvailabilityLog/doctor/{doctorId}/latest ─────────────
        /// <summary>ডাক্তারের সর্বশেষ availability লগ আনে</summary>
        [HttpGet("doctor/{doctorId:guid}/latest")]
        [ProducesResponseType(typeof(ApiResponseDto<DoctorAvailabilityLogDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetLatest(Guid doctorId, CancellationToken ct = default)
        {
            var result = await mediator.Send(new GetLatestDoctorAvailabilityLogQuery(doctorId), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── GET: api/DoctorAvailabilityLog/status/{status} ──────────────────────
        /// <summary>নির্দিষ্ট স্ট্যাটাসের লগ পেজিনেশন সহ আনে</summary>
        [HttpGet("status/{status}")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<DoctorAvailabilityLogDto>>), 200)]
        public async Task<IActionResult> GetByStatus(
            DoctorAvailabilityStatus status,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetDoctorAvailabilityLogsByStatusQuery(status, pageNumber, pageSize), ct);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── GET: api/DoctorAvailabilityLog/date-range?startDate=...&endDate=... ──
        /// <summary>নির্দিষ্ট তারিখ পরিসরের লগ আনে</summary>
        [HttpGet("date-range")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<DoctorAvailabilityLogDto>>), 200)]
        public async Task<IActionResult> GetByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(
                new GetDoctorAvailabilityLogsByDateRangeQuery(startDate, endDate, pageNumber, pageSize), ct);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── POST: api/DoctorAvailabilityLog ──────────────────────────────────────
        /// <summary>নতুন availability লগ তৈরি করে</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDto<DoctorAvailabilityLogDto>), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(
            [FromBody] CreateDoctorAvailabilityLogDto dto,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new CreateDoctorAvailabilityLogCommand(dto), ct);
            if (!result.Success)
                return BadRequest(result);

            return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result);
        }

        // ── PUT: api/DoctorAvailabilityLog/{id} ──────────────────────────────────
        /// <summary>বিদ্যমান লগ আপডেট করে</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<DoctorAvailabilityLogDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateDoctorAvailabilityLogDto dto,
            CancellationToken ct = default)
        {
            var result = await mediator.Send(new UpdateDoctorAvailabilityLogCommand(id, dto), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // ── DELETE: api/DoctorAvailabilityLog/{id} ───────────────────────────────
        /// <summary>লগ ডিলিট করে</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var result = await mediator.Send(new DeleteDoctorAvailabilityLogCommand(id), ct);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
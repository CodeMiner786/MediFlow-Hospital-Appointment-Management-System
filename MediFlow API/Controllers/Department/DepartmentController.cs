using HealthcareHospitalManagement.Application.Commands.Department;
using HealthcareHospitalManagement.Application.Common;
using HealthcareHospitalManagement.Application.DTOs.Department;
using HealthcareHospitalManagement.Application.Queries.Department;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediFlow_API.Controllers.Department
{
    [ApiController]
    [Route("api/departments")]
    public sealed class DepartmentController(IMediator mediator) : ControllerBase
    {
        // ── Commands (Create, Update, Delete) ────────────────────────────────────

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDto<DepartmentDto>), 200)]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto dto, CancellationToken ct)
            => Ok(await mediator.Send(new CreateDepartmentCommand(dto), ct));

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<DepartmentDto>), 200)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDepartmentDto dto, CancellationToken ct)
            => Ok(await mediator.Send(new UpdateDepartmentCommand(id, dto), ct));

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<bool>), 200)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
            => Ok(await mediator.Send(new DeleteDepartmentCommand(id), ct));


        // ── Queries (Read Operations) ────────────────────────────────────────────

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseDto<DepartmentDto>), 200)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
            => Ok(await mediator.Send(new GetDepartmentByIdQuery(id), ct));

        [HttpGet("by-code/{code}")]
        [ProducesResponseType(typeof(ApiResponseDto<DepartmentDto>), 200)]
        public async Task<IActionResult> GetByCode(string code, CancellationToken ct)
            => Ok(await mediator.Send(new GetDepartmentByCodeQuery(code), ct));

        [HttpGet("by-name/{name}")]
        [ProducesResponseType(typeof(ApiResponseDto<DepartmentDto>), 200)]
        public async Task<IActionResult> GetByName(string name, CancellationToken ct)
            => Ok(await mediator.Send(new GetDepartmentByNameQuery(name), ct));

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<DepartmentDto>>), 200)]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var query = new GetPagedDepartmentsQuery(pageNumber, pageSize);
            return Ok(await mediator.Send(query, ct));
        }

        [HttpGet("by-location/{location}")]
        [ProducesResponseType(typeof(ApiResponseDto<PagedResultDto<DepartmentDto>>), 200)]
        public async Task<IActionResult> GetByLocation(
            string location,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var query = new GetDepartmentsByLocationQuery(location, pageNumber, pageSize);
            return Ok(await mediator.Send(query, ct));
        }

        [HttpGet("{departmentId:guid}/with-doctors")]
        [ProducesResponseType(typeof(ApiResponseDto<DepartmentDto>), 200)]
        public async Task<IActionResult> GetWithDoctors(Guid departmentId, CancellationToken ct)
            => Ok(await mediator.Send(new GetDepartmentWithDoctorsQuery(departmentId), ct));
    }
}
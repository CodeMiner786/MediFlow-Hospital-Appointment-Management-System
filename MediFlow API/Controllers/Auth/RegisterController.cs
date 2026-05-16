//using HealthcareHospitalManagement.Application.Commands.Auth;
//using MediatR;
//using MediFlow.Service.DTOs.Auth;
//using Microsoft.AspNetCore.Mvc;

//namespace MediFlow_API.Controllers.Auth
//{
//    [ApiController]
//    [Route("api/auth")]
//    [Tags("Auth")]
//    public class RegisterController : ControllerBase
//    {
//        private readonly ISender _sender;
//        public RegisterController(ISender sender) => _sender = sender;

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto) =>
//            Ok(await _sender.Send(new RegisterCommand(dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber, dto.Password, dto.Role)));
//    }
//}

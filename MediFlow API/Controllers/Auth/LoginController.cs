//using HealthcareHospitalManagement.Application.Commands.Auth;
//using MediatR;
//using MediFlow.Service.DTOs.Auth;
//using Microsoft.AspNetCore.Mvc;

//namespace MediFlow_API.Controllers.Auth
//{
//    [ApiController]
//    [Route("api/auth")]
//    [Tags("Auth")]
//    public class LoginController : ControllerBase
//    {
//        private readonly ISender _sender;
//        public LoginController(ISender sender) => _sender = sender;

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto) =>
//            Ok(await _sender.Send(new LoginCommand(dto.Email, dto.Password)));
//    }
//}

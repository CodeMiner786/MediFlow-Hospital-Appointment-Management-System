//using HealthcareHospitalManagement.Application.Queries.Auth;
//using MediatR;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace MediFlow_API.Controllers.Auth
//{
//    [Authorize]
//    [ApiController]
//    [Route("api/auth")]
//    [Tags("Auth")]
//    public class ProfileController : ControllerBase
//    {
//        private readonly ISender _sender;
//        public ProfileController(ISender sender) => _sender = sender;

//        [HttpGet("profile")]
//        public async Task<IActionResult> GetProfile()
//        {
//            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
//            return Ok(await _sender.Send(new GetUserProfileQuery(userId)));
//        }
//    }
//}

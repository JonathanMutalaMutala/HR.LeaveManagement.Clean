using HR.LeaveManagement.Application.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authentificationService; 

        public AuthController(IAuthService authentificationService)
        {
            this._authentificationService = authentificationService;    
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await _authentificationService.Login(request));
        }
        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return Ok(await _authentificationService.Register(request));
        }

    }
}

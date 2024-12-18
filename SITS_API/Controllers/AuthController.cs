using Microsoft.AspNetCore.Mvc;
using SITS_API.Application.Contracts;
using SITS_API.Application.DTOs;

namespace SITS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUser user;

        public AuthController(IUser user)
        {
            this.user = user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LoginUserIn(LoginDTO loginDTO)
        {
            var result = await user.LoginUserAsync(loginDTO);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterUser(RegisterUserDTO registerDTO)
        {
            var result = await user.RegisterUserAsync(registerDTO);
            return Ok(result);
        }
    }
}

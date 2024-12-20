using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SITS_API.Application.Contracts;
using SITS_API.Application.DTOs;
using SITS_API.Domain.Entities;

namespace SITS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser user;

        public UsersController(IUser user)
        {
            this.user = user;
        }

        [HttpPost]
        public async Task<ActionResult<RegistrationResponse>> RegisterUser(RegisterUserDTO registerDTO)
        {
            var result = await user.RegisterUserAsync(registerDTO);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUserDetails()
        {
            var result = await user.GetUserDetails();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetUserDetailById(int id)
        {
            var result = await user.GetUserDetailsById(id);
            return Ok(result);
        }

        // PUT: api/Users/1
        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationUser>> UpdateUserDetail(int id, ApplicationUser userDetails)
        {
            if (id != userDetails.Id)
            {
                return BadRequest();
            }

            var result = await user.UpdateUser(userDetails);
            return Ok(result);
        }

    }
}

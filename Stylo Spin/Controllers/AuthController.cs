using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stylo_Spin.Dtos;
using Stylo_Spin.Helper;
using Stylo_Spin.Models;
using Stylo_Spin.Services.Defination;

namespace Stylo_Spin.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService service;

        public AuthController(IUserService service)
        {
            this.service = service;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.UserName) || string.IsNullOrEmpty(dto.Password))
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid login request"));

            var token = await service.Login(dto);

            if (token == "User not found" || token == "Invalid password")
                return Unauthorized(ApiResponse<string>.ErrorResponse(token));

            return Ok(ApiResponse<string>.SuccessResponse(token, "Login successful"));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] TblUser user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.UserEmail) || string.IsNullOrEmpty(user.Password))
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid registration request"));

            var result = await service.AddUserAsync(user);

            if (!result)
                return Conflict(ApiResponse<string>.ErrorResponse("User with this email or username already exists"));

            return CreatedAtAction(nameof(Register), new { id = user.Id },
                ApiResponse<TblUser>.SuccessResponse(user, "Registration successful"));
        }

    }
}

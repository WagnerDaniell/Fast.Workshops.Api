using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fast.Workshops.Application.UseCases.Auth;
using Fast.Workshops.Application.DTOs.Auth;

namespace Fast.Workshops.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly RegisterUseCase _registerUseCase;

        public AuthController(LoginUseCase loginUseCase, RegisterUseCase registerUseCase)
        {
            _loginUseCase = loginUseCase;
            _registerUseCase = registerUseCase;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _loginUseCase.ExecuteLogin(request);
            return Ok(result);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _registerUseCase.ExecuteRegister(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}

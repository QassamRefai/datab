using Microsoft.AspNetCore.Mvc;
using form_builder.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;

namespace form_builder.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly UserService _userService;

        public AuthController(TokenService tokenService, UserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.GetUser(request.Username, request.Password);

            if (user == null)
                return Unauthorized();

            var token = _tokenService.GenerateToken(user);

            return Ok(new { Token = token });
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.ExpiredToken))
            {
                return BadRequest("Expired token is required.");
            }

            var principal = _tokenService.GetPrincipalFromExpiredToken(request.ExpiredToken);
            if (principal == null)
            {
                return Unauthorized("Invalid or expired token.");
            }

            var username = principal.Identity.Name;

            var user = _userService.GetUserByUsername(username);
            if (user == null)
            {
                return Unauthorized("Invalid user.");
            }

            var newToken = _tokenService.GenerateToken(user);

            return Ok(new { Token = newToken });
        }
    }
    public class RefreshTokenRequest
    {
        public string ExpiredToken { get; set; }
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

using LexiQ_api.DTOs;
using LexiQ_api.Models;
using LexiQ_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace LexiQ_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private JwtService _jwtService;

        public AuthController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CustomRegisterRequest registerRequest)
        {
            var newUser = new UserModel
            {
                Email = registerRequest.Email,
                UserName = registerRequest.UserName,
            };

            var result = await _userManager.CreateAsync(newUser, registerRequest.Password);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] CustomLoginRequest loginRequest)
        {
            IActionResult response = Unauthorized();

            var user = _jwtService.AuthenticateUser(loginRequest);

            if (user != null)
            {
                var tokenString = _jwtService.GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        [Authorize]
        [HttpGet("values")]
        public ActionResult<IEnumerable<string>> GetValues()
        {
            return new string[] { "value1", "value2", "value3", "value4", "value5" };
        }

    }
}

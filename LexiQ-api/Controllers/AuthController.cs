using LexiQ_api.DTOs;
using LexiQ_api.Models;
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

        public AuthController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult>RegisterAsync([FromBody] CustomRegisterRequest registerRequest)
        {
            var newUser = new UserModel{
                Email = registerRequest.Email,
                UserName = registerRequest.UserName,
            };

            var result = await _userManager.CreateAsync(newUser, registerRequest.Password);   
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] CustomLoginRequest loginRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.PassWord, false, false);
            return Ok(result);
        }
        
    }
}

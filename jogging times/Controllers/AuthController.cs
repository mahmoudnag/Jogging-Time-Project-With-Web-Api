using jogging_times.Models;
using jogging_times.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using RestSharp;

namespace jogging_times.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;

        public AuthController(IAuthService authService, UserManager<User> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        [HttpPost("/Sign up As Admin")]

        public async Task<IActionResult> RegisterAsyncAdmin([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsyncAdmin(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("/Sign up As UserManager")]
        public async Task<IActionResult> RegisterAsyncUserManager([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsyncUserManager(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpPost("/Sign up As User")]
        public async Task<IActionResult> RegisterAsyncUser([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsyncUser(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("/Sign In")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cookie = Request.Cookies["ID"];
            if (cookie != null)
            
                return BadRequest("you are login already");
            
            
            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _authService.GetTokenAsync(model);
          
            if (!result.IsAuthenticated)
                    return BadRequest(result.Message);
      
            Response.Cookies.Append("ID", user.Id);
          
            return Ok(result);
           
        }

        [HttpGet("/LogOut")]
        public IActionResult logout()
        {
            var cookie = Request.Cookies["ID"];
            if (cookie == null)
                return BadRequest("you are loggout already");
            Response.Cookies.Delete("ID");
            string str = "logged out successfully";
            return Ok(str);
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
    }
}


using jogging_times.DTO;
using jogging_times.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace jogging_times.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> _userManager;
        public UserManagerController(ApplicationDbContext _context, UserManager<User> userManager)
        {
            context = _context;
            _userManager = userManager;
        }

       [Authorize(Roles="UserManger,Admin")]
        [HttpGet("/all_users")]

        public async Task<IActionResult> GetAllUsers()
        {
            
            var ID = Request.Cookies["ID"];
            if (ID == null)
                return BadRequest("you should login first");
            List<User> user = context.Users.OrderBy(c=>c.FirstName).ToList();
            return Ok(user);
        }



        [HttpPost("/Add_User")]
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {  
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthModel { Message = "Username is already registered!" };

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");


            return new AuthModel
            {
                Email = user.Email,
            
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
              
                Username = user.UserName
            };
        }


        [HttpPut("/update user")]
        public IActionResult UpdateUser( [FromBody] UserDTO NewUser)
        {
            var ID = Request.Cookies["ID"];
            if (ID == null)
                return BadRequest("you should login first");
            if (ModelState.IsValid)
            {
                string id = NewUser.Id;
                User olduser = context.Users.FirstOrDefault(s => s.Id == id);
                olduser.FirstName = NewUser.FirstName;
                olduser.LastName = NewUser.LastName;
                olduser.UserName = NewUser.UserName;
                olduser.Email = NewUser.Email;

                context.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent,"Data Updated");
            }

            return BadRequest("Data Not Valid");

        }


        [HttpDelete("id")]
        public IActionResult Deletejogging(string id)
        {
            var ID = Request.Cookies["ID"];
            if (ID == null)
                return BadRequest("you should login first");
            User olduser = context.Users.FirstOrDefault(s => s.Id == id);
            context.Users.Remove(olduser);
            context.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent, "Data Updated");

        }
    }
}


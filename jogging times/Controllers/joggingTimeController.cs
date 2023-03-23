using jogging_times.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using jogging_times.Models;
using Microsoft.AspNetCore.Identity;
using jogging_times.DTO;
using Microsoft.AspNetCore.Authorization;

namespace jogging_times.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class joggingTimeController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> _userManager;
        public joggingTimeController(ApplicationDbContext _context, UserManager<User> userManager)
        {
            context = _context;
            _userManager = userManager;
        }


        [Authorize(Roles ="Admin")]
        [HttpGet("/get joggingtimes for all user")]
        public List<joggingTime> joggingTimes_For_All_User() {
           
            List<joggingTime>jogging= context.joggingTimes.OrderBy(c=>c.date).ToList();
            return jogging;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> get_AJoggingTime(int id)
        {
            
            var cookie = Request.Cookies["ID"];
            if (cookie == null)
                return BadRequest("you should login first");

            joggingTime joggingtime = context.joggingTimes.FirstOrDefault(c => c.Id == id);
            return Ok(joggingtime);
        }

        [HttpGet("/get joggingtimes for user")]
        public async Task<IActionResult> joggingTimes_For_User()
        {
            
            var ID = Request.Cookies["ID"];
            if (ID == null)
                return BadRequest("you should login first");

           List<joggingTime> times =context.joggingTimes.Where(c => c.UserId == ID).ToList();
            return Ok(times);
        }


        [HttpPost("/Add joggingtime")]
        public async Task<IActionResult> insert_joggingtime( UserWithJoggingDTO jogging)
        {
         
            var ID = Request.Cookies["ID"];
            if (ID == null)
                return BadRequest("you should login first");
            joggingTime main = new joggingTime();
            main.date = jogging.date;
            main.time = jogging.time;
            main.distance = jogging.distance;
            main.UserId = ID;
            context.joggingTimes.Add(main);
            context.SaveChanges();
            return Ok(main);
        }


        [HttpGet]
        [Route("/Report to averge of speed per distance")]
        public IActionResult ReportWeek()
        {
          
            var ID = Request.Cookies["ID"];
            if (ID == null)
                return BadRequest("you should login first");
            List<joggingTime> joggingTimesR = context.joggingTimes.Where(s => s.UserId ==ID).ToList();
            double distance = joggingTimesR.Sum(s => s.distance);
            double time = (joggingTimesR.Sum(s => s.time));
            double AvergeSpeed = ((distance / time)/7);
            double AvergeDistance = ((joggingTimesR.Sum(s => s.distance)) / 7);



            string data =  "averge speed is: "+ AvergeSpeed+" averge distance is: "+ AvergeDistance;

            return Ok(data);

        }


        [HttpPut("{id:int}")] 
        public IActionResult Update_JoggingTime([FromRoute] int id, [FromBody] UserWithJoggingDTO Newjogging)
        {
            var ID = Request.Cookies["ID"];
            if (ID == null)
                return BadRequest("you should login first");

            if (ModelState.IsValid)
            {
                joggingTime oldjogging = context.joggingTimes.FirstOrDefault(s => s.Id == id);
                oldjogging.date = Newjogging.date;
                oldjogging.time = Newjogging.time;
                oldjogging.distance = Newjogging.distance;
                context.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent, "Data Updated");
            }

            return BadRequest("Data Not Valid");

        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete_joggingTime(int id)
        {
            var ID = Request.Cookies["ID"];
            if (ID == null)
                return BadRequest("you should login first");
            joggingTime oldjogging = context.joggingTimes.FirstOrDefault(s => s.Id == id);
            context.joggingTimes.Remove(oldjogging);
            context.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent, "Data Updated");


        }
    }
}

using Microsoft.AspNetCore.Mvc;
using UniscoLMS.DataBaseModels;
using ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using UniscoLMS.ViewModels;
using UniscoLMS.Errors;

namespace UniscoLMS.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly UniscoDbContext _UniscoDbContext;

        public CourseController(UniscoDbContext UniscoDbContext)
        {
            _UniscoDbContext = UniscoDbContext;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse([FromBody] Course Course)
        {
            // get logged user...
            var userName = GetUserLogin();
            var loggedUser = await _UniscoDbContext.Users.Where(x => x.Username == userName).FirstOrDefaultAsync();
            if (loggedUser == null)
                throw new ErrorUserNotFound();

            _UniscoDbContext.Courses.Add(Course);

            await _UniscoDbContext.SaveChangesAsync();

            
            return Ok(new SuccessModel(true));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("GetCourse")]
        public async Task<IActionResult> GetCourse()
        {
            var courses = await _UniscoDbContext.Courses.ToListAsync();
            return Ok(new SuccessModel(courses));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("UserCourses")]
        public async Task<IActionResult> UserCourses()
        {
            // get logged user...
            var userName = GetUserLogin();
            var loggedUser = await _UniscoDbContext.Users.Where(x => x.Username == userName).FirstOrDefaultAsync();
            if (loggedUser == null)
                throw new ErrorUserNotFound();

            var Courses = await _UniscoDbContext.Courses.Where(u => u.UserCourses.Any(x=> x.LearnerId == loggedUser.UserId)).ToListAsync();
            return Ok(new SuccessModel(Courses));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("CourseSearch")]
        public async Task<IActionResult> CourseSearch(string Name)
        {
            var tags = await _UniscoDbContext.Courses.Where(t => t.Name.Contains(Name)).ToListAsync();
            return Ok(new SuccessModel(tags));
        }

        private string GetUserLogin()
        {
            var token = HttpContext.User.Identity;
            var userName = token?.Name;
            if (string.IsNullOrEmpty(userName))
                return null;
            else
                return userName;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniscoLMS.DataBaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ViewModels;
using UniscoLMS.Enums;
using UniscoLMS.ViewModels;
using UniscoLMS.Errors;

namespace UniscoLMS.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UniscoDbContext _UniscoDbContext;

        public UserController(UniscoDbContext UniscoDbContext)
        {
            _UniscoDbContext = UniscoDbContext ?? throw new ArgumentNullException(nameof(UniscoDbContext));
        }

        [HttpGet("getBio")]
        public async Task<IActionResult> GetBio()
        {
            var userName = GetCurrentUserId();
            var user = await _UniscoDbContext.Users.FirstOrDefaultAsync(u => u.Username == userName);

            if (user == null)
            {
                throw new ErrorUserNotFound();
            }

            return Ok(new SuccessModel(user));
        }

        [HttpPut("updateBio")]
        public async Task<IActionResult> UpdateBio([FromBody] BioRequest bioRequest)
        {
            var userName = GetCurrentUserId();
            var user = await _UniscoDbContext.Users.FirstOrDefaultAsync(u => u.Username == userName);

            if (user == null)
            {
                throw new ErrorUserNotFound();
            }

            user.Bio = bioRequest.bio;
            await _UniscoDbContext.SaveChangesAsync();

            return Ok(new SuccessBooleanModel(true));
        }

        [HttpDelete("deleteBio")]
        public async Task<IActionResult> DeleteBio()
        {
            var userName = GetCurrentUserId();
            var user = await _UniscoDbContext.Users.FirstOrDefaultAsync(u => u.Username == userName);

            if (user == null)
            {
                throw new ErrorUserNotFound();
            }

            // Soft delete bio by setting it to null
            user.Bio = null;
            await _UniscoDbContext.SaveChangesAsync();

            return Ok(new SuccessBooleanModel(true));
        }

        [HttpGet("getProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userName = GetCurrentUserId();
            var user = await _UniscoDbContext.Users
                .FirstOrDefaultAsync(u => u.Username == userName);

            if (user == null)
            {
                throw new ErrorUserNotFound();
            }

            var userProfileResponse = new UserProfileResponse
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                GoogleId = user.GoogleId,
                Mobile = user.Mobile,
                EmailValidation = user.EmailValidation,
                PhoneValidation = user.PhoneValidation,
                Bio = user.Bio,
                Approved = user.Approved,
                Verified = user.Verified,
                
            };

            return Ok(new SuccessModel(userProfileResponse));
        }

        [HttpGet("getAllExperts")]
        [AllowAnonymous] // This allows anyone to access the endpoint
        public async Task<IActionResult> GetAllExperts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var experts = await _UniscoDbContext.Users
                .Where(u => u.RoleId == (int)Roles.expert)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var expertProfiles = experts.Select(user => new UserProfileResponse
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                GoogleId = user.GoogleId,
                Mobile = user.Mobile,
                EmailValidation = user.EmailValidation,
                PhoneValidation = user.PhoneValidation,
                Bio = user.Bio,
                Approved = user.Approved,
                Verified = user.Verified,

            }).ToList();

            return Ok(new SuccessModel(expertProfiles));
        }

        private string GetCurrentUserId()
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

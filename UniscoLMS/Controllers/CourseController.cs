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
    public class TagsController : ControllerBase
    {
        private readonly UniscoDbContext _UniscoDbContext;

        public TagsController(UniscoDbContext UniscoDbContext)
        {
            _UniscoDbContext = UniscoDbContext;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("AddEditTags")]
        public async Task<IActionResult> AddEditTags([FromBody] AddTagsRequest AddTagsRequest)
        {
            // get logged user...
            var userName = GetUserLogin();
            var loggedUser = await _UniscoDbContext.Users.Where(x => x.Username == userName).FirstOrDefaultAsync();
            if (loggedUser == null)
                throw new ErrorUserNotFound();

            var userTags = await _UniscoDbContext.UserTags.Where(x => x.UserId == loggedUser.UserId).ToListAsync();
            List<UserTag> newTags = new List<UserTag>();
            foreach (var tagId in AddTagsRequest.Ids)
            {
                newTags.Add(new UserTag
                {
                    UserId = loggedUser.UserId,
                    TagId = tagId
                });
            }
            if (userTags == null)
            {
                await _UniscoDbContext.UserTags.AddRangeAsync(newTags);
            }
            else
            {
                _UniscoDbContext.RemoveRange(userTags);
                await _UniscoDbContext.UserTags.AddRangeAsync(newTags);
            }

            await _UniscoDbContext.SaveChangesAsync();

            var result = newTags.Select(x => new TagResponse()
            {
                TagId = x.TagId,
                TagName = x.Tag.TagName
            }).ToList();
            return Ok(new SuccessModel(result));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("GetTags")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _UniscoDbContext.Tags.Select(x => new TagResponse()
            {
                TagId = x.Id,
                TagName = x.TagName
            }).ToListAsync();
            return Ok(new SuccessModel(tags));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("UserTags")]
        public async Task<IActionResult> GetTagsByUser()
        {
            // get logged user...
            var userName = GetUserLogin();
            var loggedUser = await _UniscoDbContext.Users.Where(x => x.Username == userName).FirstOrDefaultAsync();
            if (loggedUser == null)
                throw new ErrorUserNotFound();

            var tags = await _UniscoDbContext.UserTags.Where(u => u.UserId == loggedUser.UserId).Select(x => new TagResponse()
            {
                TagId = x.Tag.Id,
                TagName = x.Tag.TagName
            }).ToListAsync();
            return Ok(new SuccessModel(tags));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("TagsSearch")]
        public async Task<IActionResult> TagsSearch(string tag)
        {
            var tags = await _UniscoDbContext.Tags.Where(t => t.TagName.Contains(tag)).Select(x => new TagResponse()
            {
                TagId = x.Id,
                TagName = x.TagName
            }).ToListAsync();
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

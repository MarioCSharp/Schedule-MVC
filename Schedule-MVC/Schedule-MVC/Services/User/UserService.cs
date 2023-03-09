using Microsoft.AspNetCore.Http;
using Schedule_MVC.Data;
using Schedule_MVC.Models.Admin;
using Schedule_MVC.Services.User;
using System.Security.Claims;

namespace Schedule_MVC.Services.Approved
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _dbContext;
        private IHttpContextAccessor httpContext;
        public UserService(ApplicationDbContext _dbContext
                           , IHttpContextAccessor httpContext)
        {
            this._dbContext = _dbContext;
            this.httpContext = httpContext;
        }

        public List<UserDisplayModel> GetNonApproved()
        {
            return _dbContext.Users
                .Where(x => !_dbContext.Approved.Any(y => y.UserId == x.Id))
              .Select(x => new UserDisplayModel { Id = x.Id, Email = x.Email, Name = x.FullName })
                .ToList();
        }

        public string GetUserId()
        {
            return httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool IsAdmin(string id = null)
        {
            if (id == null)
            {
                id = GetUserId();
            }

            if (id == null)
            {
                return false;
            }

            return _dbContext.Users.FirstOrDefault(x => x.Id == id).Email == "mario.18@abv.bg";
        }

        public bool IsApproved(string id = null)
        {
            if (id == null)
            {
                id = GetUserId();
            }

            if (id == null)
            {
                return false;
            }

            return _dbContext.Approved.Any(x => x.UserId == id) || IsAdmin();
        }
    }
}

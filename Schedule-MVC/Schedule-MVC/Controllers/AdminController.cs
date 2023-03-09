using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule_MVC.Data;
using Schedule_MVC.Data.Models;
using Schedule_MVC.Models.Admin;
using Schedule_MVC.Services.User;

namespace Schedule_MVC.Controllers
{
    public class AdminController : Controller
    {
        private IUserService _userService;
        private ApplicationDbContext _context;
        public AdminController(IUserService userService,
                               ApplicationDbContext context)
        {
            this._userService = userService;
            this._context = context;
        }
        [Authorize]
        public IActionResult Approve()
        {
            if (!_userService.IsAdmin())
            {
                return BadRequest();
            }

            return View(new ApproveForm
            {
                UsersNotApproved = _userService.GetNonApproved()
            });
        }
        [Authorize]
        [HttpPost]
        public IActionResult Approve(string id)
        {
            if (!_userService.IsAdmin())
            {
                return BadRequest();
            }

            _context.Approved.Add(new Approved
            {
                UserId = id
            });

            _context.SaveChanges();

            return RedirectToAction("Approve", "Admin");
        }
    }
}

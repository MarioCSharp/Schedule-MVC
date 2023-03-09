using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule_MVC.Models;
using Schedule_MVC.Services.User;
using System.Diagnostics;

namespace Schedule_MVC.Controllers
{
    public class HomeController : Controller
    {
        private IUserService userService;
        public HomeController(IUserService userService)
        {
            this.userService = userService; 
        }

        [Authorize]

        public IActionResult Index()
        {
            if (userService.IsApproved() == false)
            {
                return RedirectToAction("NotApproved", "Home");
            }

            return View();
        }
        [Authorize]
        public IActionResult NotApproved()
        {
            return View();
        }
        [Authorize]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
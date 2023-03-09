using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Schedule_MVC.Data;
using Schedule_MVC.Data.Models;
using Schedule_MVC.Models.Work;
using Schedule_MVC.Services.User;
using Schedule_MVC.Services.Work;

namespace Schedule_MVC.Controllers
{
    public class WorkController : Controller
    {
        private ApplicationDbContext _context;
        private IUserService _userService;
        private IWorkService _workService;
        public WorkController(ApplicationDbContext _context,
                              IUserService _userService,
                              IWorkService _workService)
        {
            this._context = _context;
            this._workService = _workService;
            this._userService = _userService;
        }
        [Authorize]
        public IActionResult Create()
        {
            if (!_userService.IsApproved())
            {
                return RedirectToAction("NotApproved", "Home");
            }

            return View(new CreateWorkModel
            {
                Classes = _context.Grades.Select(x => new ClassDisplayModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateWorkModel model)
        {
            if (!_userService.IsApproved())
            {
                return RedirectToAction("NotApproved", "Home");
            }

            if (!(model.ClassId >= 1 && model.ClassId <= 20 && !string.IsNullOrEmpty(model.Subject)))
            {
                model.Classes = _context.Grades.Select(x => new ClassDisplayModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

                return View(model);
            }

            var added = _workService.IsAcceptable(model.Date, model.ClassId);

            if (!added)
            {
                model.Error = "Класът вече е достигнал лимита си за тестове тази седмица! Опитайте с нова дата!";
                model.Classes = _context.Grades.Select(x => new ClassDisplayModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
                return View(model);
            }

            _context.Works.Add(new Work
            {
                Subject = model.Subject,
                ClassId = model.ClassId,
                Date = model.Date,
            });

            _context.SaveChanges();

            return RedirectToAction("Created", "Work");
        }

        public IActionResult Created() { return View(); }
    }
}

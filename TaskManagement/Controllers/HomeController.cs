using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskMDbContext dbContext;

        public HomeController(TaskMDbContext context)
        {
            dbContext = context;
        }

        public IActionResult EmployeeHome()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            ViewBag.CurrentUser = user;
            return View();
        }

        public IActionResult MangerHome()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userId);
            ViewBag.CurrentUser = user;
            return View();
        }
    }
}

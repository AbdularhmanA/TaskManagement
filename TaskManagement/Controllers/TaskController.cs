using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.ViewModel;

namespace TaskManagement.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskMDbContext dbContext;

        public TaskController(TaskMDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet]
        public async Task<IActionResult> AddTask()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await dbContext.Users.FindAsync(userId);
            ViewBag.CurrentUser = user;

            if (user?.JobPosition != "مدير")
            {
                return Forbid();
            }

            var users = await dbContext.Users
                .Where(u => u.JobPosition == "موظف")
                .Select(u => new { u.Id, u.FullName, u.JobTitle })
                .ToListAsync();

            var viewModel = new AddTaskViewModel
            {
                AvailableRecipients = users
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id,
                        Text = $"{u.JobTitle} - {u.FullName}"
                    })
                    .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(AddTaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var task = new Tasks
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Priority = viewModel.Priority,
                    Deadline = viewModel.Deadline,
                    RecipientId = viewModel.SelectedRecipientId,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                await dbContext.Tasks.AddAsync(task);
                await dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "تم إرسال المهمة بنجاح!";
                return RedirectToAction("AddTask");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> TaskReception()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await dbContext.Users.FindAsync(userId);
            ViewBag.CurrentUser = user;

            if (user?.JobPosition != "موظف")
            {
                return Forbid();
            }

            var tasks = await dbContext.Tasks
                .Include(t => t.User)
                .Where(t => t.RecipientId == userId &&
                            t.Status != "مرفوضة" &&
                            t.Status != "مقبولة")
                .Select(t => new TaskReceptionViewModel
                {
                    SenderFullName = $"{t.User.FullName} - {t.User.JobTitle}",
                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority,
                    Deadline = t.Deadline,
                    TaskId = t.TaskId
                })
                .ToListAsync();

            return View(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptTask(int taskId)
        {
            var task = await dbContext.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.Status = "مقبولة";
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("TaskReception");
        }

        [HttpPost]
        public async Task<IActionResult> RejectTask(int taskId)
        {
            var task = await dbContext.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.Status = "مرفوضة";
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("TaskReception");
        }

        [HttpGet]
        public async Task<IActionResult> TrackTask()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await dbContext.Users.FindAsync(userId);
            ViewBag.CurrentUser = user;

            if (user == null || user.JobPosition != "موظف")
            {
                return Forbid();
            }

            var tasks = await dbContext.Tasks
                .Where(t => t.RecipientId == userId &&
                            t.Track != "منجزة" &&
                            t.Track != "متأخرة تم الإنجاز" &&
                            t.Track != "متأخرة لم يتم الإنجاز" &&
                            t.Track != "غير منجزة" &&
                            t.Status == "مقبولة")
                .Select(t => new TrackTaskViewModel
                {
                    TaskId = t.TaskId,
                    Title = t.Title,
                    Priority = t.Priority,
                    Deadline = t.Deadline,
                    Track = GetTaskStatus(t.Deadline, t.Track)
                })
                .ToListAsync();

            return View(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTaskStatus(int taskId, string updatedStatus)
        {
            var task = await dbContext.Tasks.FindAsync(taskId);

            if (task != null)
            {
                var currentDate = DateTime.Now;

                if (updatedStatus == "منجزة")
                {
                    if (task.Deadline == null || task.Deadline >= currentDate)
                    {
                        task.Track = "منجزة";
                    }
                    else
                    {
                        task.Track = "متأخرة تم الإنجاز";
                    }
                }
                else if (updatedStatus == "غير منجزة")
                {
                    if (task.Deadline.HasValue && currentDate > task.Deadline)
                    {
                        task.Track = "متأخرة لم يتم الإنجاز";
                    }
                    else
                    {
                        task.Track = "غير منجزة";
                    }
                }

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("TrackTask");
        }

        [HttpGet]
        public async Task<IActionResult> TaskList()
        {
            var userId = User.Identity.Name;
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userId);
            ViewBag.CurrentUser = user;

            if (user == null || user.JobPosition != "مدير")
            {
                return Forbid();
            }

            var tasks = await dbContext.Tasks
                .Include(t => t.Recipient)
                .Where(t => t.UserId == user.Id)
                .Select(t => new TaskListViewModel
                {
                    TaskId = t.TaskId,
                    RecipientFullName = $"{t.Recipient.FullName} - {t.Recipient.JobTitle}",
                    Title = t.Title,
                    Priority = t.Priority,
                    Deadline = t.Deadline,
                    Status = GetTaskStatus(t.Deadline, t.Track)
                })
                .ToListAsync();

            return View(tasks);
        }

        public static string GetTaskStatus(DateTime? deadline, string currentStatus)
        {
            var currentDate = DateTime.Now;

            if (currentStatus == "منجزة")
            {
                return "منجزة";
            }

            if (currentStatus == "متأخرة تم الإنجاز")
            {
                return "متأخرة تم الإنجاز";
            }

            if (currentStatus == "غير منجزة")
            {
                return "غير منجزة";
            }

            if (deadline.HasValue && currentDate > deadline)
            {
                return "متأخرة لم يتم الإنجاز";
            }

            return "غير منجزة";
        }
    }
}

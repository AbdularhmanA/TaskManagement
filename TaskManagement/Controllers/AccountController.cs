using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Models;
using TaskManagement.ViewModel;

public class AccountController : Controller
{
    private readonly SignInManager<Users> signInManager;
    private readonly UserManager<Users> userManager;

    public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user.JobPosition == "مدير")
                {
                    return RedirectToAction("AddTask", "Task");
                }
                else if (user.JobPosition == "موظف")
                {
                    return RedirectToAction("TaskReception", "Task");
                }
            }
            else
            {
                ModelState.AddModelError("", "البريد الإلكتروني أو كلمة المرور غير صحيحة.");
                return View(model);
            }
        }
        return View(model);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            Users users = new Users
            {
                Id = model.UserID,
                FullName = model.FullName,
                JobTitle = model.jobtitle,
                JobPosition = model.JobPosition,
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await userManager.CreateAsync(users, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }
        return View(model);
    }

    public IActionResult VerifyEmail()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "لايوجد حساب على هذا الايميل ");
                return View(model);
            }
            else
            {
                return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
            }
        }
        return View(model);
    }

    public IActionResult ChangePassword(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("VerifyEmail", "Account");
        }
        return View(new ChangePasswordViewModel { Email = username });
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                var passwordValidator = new PasswordValidator<Users>();
                var validation = await passwordValidator.ValidateAsync(userManager, user, model.NewPassword);

                if (!validation.Succeeded)
                {
                    foreach (var error in validation.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                var result = await userManager.RemovePasswordAsync(user);
                if (result.Succeeded)
                {
                    result = await userManager.AddPasswordAsync(user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                ModelState.AddModelError("", "Email not found!");
            }
        }
        else
        {
            ModelState.AddModelError("", "Something went wrong. Try again.");
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    public IActionResult HomePage()
    {
        return View();
    }
}

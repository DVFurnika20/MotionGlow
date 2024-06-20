using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotionGlow.Models;
using MotionGlow.BLL.IServices;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotionGlow.DAL.Models;

namespace MotionGlow.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _usersService;

        public AccountController(IUserService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Views/LoginSignup/LogIn.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _usersService.GetUserByEmailAsync(model.Email);
                if (user != null && user.Password == model.Password)
                {
                    // Log the user in and redirect to a different page
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View("Views/LoginSignup/SignUp.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _usersService.GetUserByEmailAsync(model.Email);
                if (existingUser == null)
                {
                    var user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Password = model.Password // Replace with actual password hashing
                    };

                    await _usersService.AddUserAsync(user);

                    // Log the user in and redirect to a different page
                }
                else
                {
                    ModelState.AddModelError("", "Email is already in use.");
                }
            }

            return View(model);
        }
    }
}
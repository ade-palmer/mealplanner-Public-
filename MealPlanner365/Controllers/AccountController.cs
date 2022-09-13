using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealPlanner365.Models;
using MealPlanner365.Models.ViewModels;
using MealPlanner365.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MealPlanner365.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ISettingsRepository settingsRepository;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ISettingsRepository settingsRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.settingsRepository = settingsRepository;
        }


        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginViewModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(loginViewModel);
                }

                var result = await signInManager.PasswordSignInAsync(user.UserName, loginViewModel.Password, loginViewModel.RememberMe, true);

                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl ?? $"/MealPlan/{user.MealPlanId.ToString()}");
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Your account is locked out. Please try again latter or use the 'Forgot your Password' option to reset your password");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please ensure your username and password are correct and that you have verified your account via the confirmation email");
                }
            }

            return View(loginViewModel);
        }


        public IActionResult CreateDiner(Guid mealPlanId)
        {
            ViewData["MealPlan"] = mealPlanId;

            var newUserViewModel = new NewUserViewModel()
            {
                MealPlanId = mealPlanId
            };

            return View(newUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiner(NewUserViewModel newUserViewModel)
        {
            ViewData["MealPlan"] = newUserViewModel.MealPlanId;

            if (!ModelState.IsValid)
            {
                return View(newUserViewModel);
            }

            var diner = new ApplicationUser
            {
                UserName = newUserViewModel.UserName,
                Email =  newUserViewModel.Email,
                MealPlanId = newUserViewModel.MealPlanId
            };

            var result = await userManager.CreateAsync(diner, newUserViewModel.Password);

            if (result.Succeeded)
            {
                if (newUserViewModel.Administrator)
                {
                    const string RoleName = "Administrator";

                    var user = await userManager.FindByEmailAsync(newUserViewModel.Email);
                    await userManager.AddToRoleAsync(user, RoleName);
                }

                return RedirectToAction("Index", "Settings", new { mealPlanId = newUserViewModel.MealPlanId });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(newUserViewModel);
        }


        public async Task<IActionResult> EditDiner(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel()
            {
                UserId = user.Id,
                Name = user.UserName,
                Email = user.Email,
                MealPlanId = (Guid)user.MealPlanId, //TODO: Do I need this explicit casting. Can I change the Model.
            };

            ViewData["MealPlan"] = user.MealPlanId;

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditDiner(UserViewModel userViewModel)
        {
            ViewData["MealPlan"] = userViewModel.MealPlanId;

            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            var user = await userManager.FindByIdAsync(userViewModel.UserId);

            if (user == null)
            {
                return View("NotFound");
            }

            user.UserName = userViewModel.Name;
            user.Email = userViewModel.Email;

            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error updating user with ID: '{userViewModel.UserId}'");
            }

            return RedirectToAction("Index", "Settings", new { mealPlanId = user.MealPlanId });
        }



        public async Task<IActionResult> DeleteDiner(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel()
            {
                UserId = user.Id,
                Name = user.UserName,
                Email = user.Email,
                MealPlanId = (Guid)user.MealPlanId //TODO: Do I need this explicit casting. Can I change the Model.
            };

            ViewData["MealPlan"] = user.MealPlanId;

            return View(userViewModel);
        }


        [HttpPost, ActionName("DeleteDiner")]
        public async Task<IActionResult> DeleteDinerConfirmed(UserViewModel userViewModel) // TODO: Should this be an object or UserId?
        {
            ViewData["MealPlan"] = userViewModel.MealPlanId;

            var user = await userManager.FindByIdAsync(userViewModel.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error deleting user with ID: '{userViewModel.UserId}'");
            }

            return RedirectToAction("Index", "Settings", new { mealPlanId = user.MealPlanId });
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

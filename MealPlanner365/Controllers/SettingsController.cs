using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealPlanner365.Models;
using MealPlanner365.Models.ViewModels;
using MealPlanner365.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MealPlanner365.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ISettingsRepository settingsRepository;
        private readonly IOptionsSnapshot<MealPlanSettings> settings;

        public SettingsController(ISettingsRepository settingsRepository, IOptionsSnapshot<MealPlanSettings> settings)
        {
            this.settingsRepository = settingsRepository;
            this.settings = settings;
        }

        public async Task<IActionResult> Index(Guid mealPlanId)
        {
            ViewData["MealPlan"] = mealPlanId;

            var diners = await settingsRepository.GetMealPlanDiners(mealPlanId);

            var userViewModel = new List<UserViewModel>();

            foreach (var diner in diners)
            {
                userViewModel.Add(new UserViewModel()
                {
                    UserId = diner.Id,
                    Name = diner.UserName,
                    EmailConfirmed = diner.EmailConfirmed,
                    LockedOut = diner.LockoutEnabled,
                    Administrator = await settingsRepository.IsAdministrator(diner.Id)
                });
            }

            var mealPlanSettings = new SettingsViewModel()
            {
                Url = settings.Value.Url,
                DisplayDays = settings.Value.DisplayDays,
                PageIncrements = settings.Value.PageIncrements,
                Diners = userViewModel
            };

            return View(mealPlanSettings);
        }
    }
}

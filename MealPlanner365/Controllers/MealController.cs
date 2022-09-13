using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealPlanner365.Models;
using MealPlanner365.Models.ViewModels;
using MealPlanner365.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MealPlanner365.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealRepository mealRepository;

        public MealController(IMealRepository mealRepository)
        {
            this.mealRepository = mealRepository;
        }

        public async Task<IActionResult> Index(Guid mealPlanId)
        {
            var mealPlanExists = await mealRepository.MealPlanExists(mealPlanId);

            if (!mealPlanExists)
            {
                return NotFound();
            }

            var futureMeals = await mealRepository.GetFutureMeals(mealPlanId);

            // 'Group By' moved here as EF Core 3 doesn't like it
            var groupByMealDate = from m in futureMeals
                               group m by m.Date into groupedMeals
                               where groupedMeals.Count() > 1
                               select groupedMeals.Select(m => new AdditionalMealViewModel()
                               {
                                   MealId = m.MealId,
                                   Date = m.Date,
                                   MealPlanId = m.MealPlanId,
                                   Items = m.MealItems.Select(i => i.Item.Name)
                               });

            var additionalMealViewModel = new List<AdditionalMealViewModel>();

            foreach (var dateGroup in groupByMealDate)
            {
                foreach (var meal in dateGroup)
                {
                    additionalMealViewModel.Add(new AdditionalMealViewModel()
                    {
                        MealId = meal.MealId,
                        Date = meal.Date,
                        Items = meal.Items,
                        MealPlanId = meal.MealPlanId
                    });
                }
            }

            ViewData["MealPlan"] = mealPlanId;

            return View(additionalMealViewModel);
        }

        public IActionResult CreateAdditionalMeal(Guid mealPlanId) 
        {
            var additionalMeal = new AdditionalMealViewModel()
            {
                MealId = Guid.NewGuid(),
                Date = DateTimeOffset.UtcNow,
                MealPlanId = mealPlanId
            };

            ViewData["MealPlan"] = mealPlanId;

            return View(additionalMeal);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdditionalMeal(AdditionalMealViewModel additionalMealViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(additionalMealViewModel);
            }

            if (additionalMealViewModel.Date.Date < DateTimeOffset.UtcNow.Date)
            {
                ModelState.AddModelError(string.Empty, "This date is in the past. Please add a valid future date");
                return View(additionalMealViewModel);
            }

            var additionalMeal = new Meal()
            {
                MealId = additionalMealViewModel.MealId,
                Date = additionalMealViewModel.Date,
                MealPlanId = additionalMealViewModel.MealPlanId
            };

            mealRepository.AddMeal(additionalMeal);

            await mealRepository.SaveChangesAsync();

            return RedirectToAction(actionName: additionalMealViewModel.MealPlanId.ToString(),
                                    controllerName: "Meal");
        }


        public async Task<IActionResult> DeleteAdditionalMeal(Guid mealId)
        {
            if (mealId == Guid.Empty)
            {
                return NotFound();
            }

            var meal = await mealRepository.GetMealWithItemsById(mealId);

            if (meal == null)
            {
                return NotFound();
            }

            var mealToDelete = new AdditionalMealViewModel()
            {
                MealId = mealId,
                Date = meal.Date,
                MealPlanId = meal.MealPlanId
            };

            ViewData["MealPlan"] = meal.MealPlanId;

            return View(mealToDelete);
        }


        [HttpPost, ActionName("DeleteAdditionalMeal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAdditionalMealConfirmed(Guid mealId)
        {
            var mealToDelete = await mealRepository.GetMealById(mealId);

            mealRepository.DeleteMeal(mealToDelete);

            await mealRepository.SaveChangesAsync();

            return RedirectToAction(actionName: mealToDelete.MealPlanId.ToString(),
                                    controllerName: "Meal");
        }
    }
}

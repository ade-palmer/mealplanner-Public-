using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealPlanner365.Models;
using MealPlanner365.Models.ViewModels;
using MealPlanner365.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;
using MealPlanner365.Services.Interfaces;

namespace MealPlanner365.Controllers
{
    public class MealPlanController : Controller
    {
        // TODO: Check if deleting a user deletes a MealPlan and cascades
        // Also check if it deletes a user from a meal which it should but leave meal and plan in place

        private readonly IMealPlanRepository mealPlanRepository;
        private readonly IShoppingRepository shoppingRepository;
        private readonly IItemRepository itemRepository;
        private readonly IOptionsSnapshot<MealPlanSettings> settings;

        public MealPlanController(IMealPlanRepository mealPlanRepository, 
                                  IShoppingRepository shoppingRepository, 
                                  IItemRepository itemRepository,
                                  IOptionsSnapshot<MealPlanSettings> settings)
        {
            this.mealPlanRepository = mealPlanRepository;
            this.shoppingRepository = shoppingRepository;
            this.itemRepository = itemRepository;
            this.settings = settings;
        }

        public async Task<IActionResult> Index(Guid mealPlanId, int pageOffset = 0)
        {
            var mealPlanExist = await mealPlanRepository.MealPlanExists(mealPlanId);

            if (!mealPlanExist)
            {
                return NotFound();
            }

            var dateWithPageOffset = DateTimeOffset.UtcNow.AddDays(pageOffset * settings.Value.PageIncrements);

            var mealPlanWeekStartDay = await mealPlanRepository.GetMealPlanWeekStartDay(mealPlanId);

            var firstDayOfWeek = GetFirstDayOfWeek(dateWithPageOffset, mealPlanWeekStartDay);

            var meals = await mealPlanRepository.GetMeals(mealPlanId, firstDayOfWeek, settings.Value.DisplayDays);

            var shoppingDays = await shoppingRepository.GetShoppingByDate(mealPlanId, firstDayOfWeek, settings.Value.DisplayDays);

            var mealViewModel = new List<MealViewModel>();
            var repeatingDay = new DateTimeOffset();
            foreach (var meal in meals)
            {
                mealViewModel.Add(new MealViewModel()
                {
                    MealId = meal.MealId,
                    Date = meal.Date,
                    DisplayDate = (meal.Date == repeatingDay) ? false : true,
                    ShoppingDay = shoppingDays.Any(d => d.Date == meal.Date),
                    SelectedItems = meal.MealItems.Select(i => i.ItemId),
                    Diners = meal.UserMeals.Select(u => u.UserId.ToString())
                });
                repeatingDay = meal.Date;
            }

            // Populate missing meal dates with holding entry
            var exitingMealDates = meals.Select(d => d.Date);
            for (int i = 0; i < settings.Value.DisplayDays; i++)
            {
                if (!exitingMealDates.Contains(firstDayOfWeek.AddDays(i)))
                {
                    mealViewModel.Add(new MealViewModel()
                    {
                        MealId = Guid.NewGuid(),
                        Date = firstDayOfWeek.AddDays(i),
                        DisplayDate = true,
                        ShoppingDay = shoppingDays.Any(d => d.Date == firstDayOfWeek.AddDays(i))
                    });
                }
            }

            var mealPlanViewModel = new MealPlanViewModel()
            {
                PageOffset = pageOffset,
                MealPlanId = mealPlanId,
                Meals = mealViewModel.OrderBy(d => d.Date).ToList()
            };


            var selectListItems = await itemRepository.GetSelectListItems(mealPlanId);
            ViewBag.AllSelectListItems = new SelectList(selectListItems, "ItemId", "Name", null, "FoodType.Name");

            var mealPlanDiners = await mealPlanRepository.GetMealPlanDiners(mealPlanId);
            ViewBag.mealPlanDiners = new SelectList(mealPlanDiners, "Id", "UserName");

            return View(mealPlanViewModel);
        }

        private DateTimeOffset GetFirstDayOfWeek(DateTimeOffset dayInWeek, DayOfWeek firstDay)
        {
            DateTimeOffset firstDayInWeek = dayInWeek.Date;

            while (firstDayInWeek.DayOfWeek != firstDay)
            {
                firstDayInWeek = firstDayInWeek.AddDays(-1);
            }

            return firstDayInWeek;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMeals(MealPlanViewModel mealPlanViewModel)
        {
            // TODO: Do I need to check the Model is OK?

            foreach (var meal in mealPlanViewModel.Meals)
            {
                var submittedMealItems = new List<MealItem>();
                GetSubmittedMealItems(submittedMealItems, meal);

                var submittedMealDiners = new List<UserMeal>();
                GetSubmittedMealDiners(submittedMealDiners, meal);

                if (submittedMealItems.Any() || submittedMealDiners.Any())
                {
                    await CheckIfNewMealRequired(meal, mealPlanViewModel.MealPlanId);
                } 

                await UpdateMealItems(submittedMealItems, meal.MealId);

                await UpdateMealDiners(submittedMealDiners, meal.MealId);
            }

            await mealPlanRepository.SaveChangesAsync();

            return RedirectToAction(actionName: mealPlanViewModel.MealPlanId.ToString(),
                                    controllerName: "MealPlan");
        }

        private void GetSubmittedMealItems(List<MealItem> submittedMealItems, MealViewModel meal)
        {
            if (meal.SelectedItems != null)
            {
                foreach (var selectedItem in meal.SelectedItems)
                {
                    submittedMealItems.Add(new MealItem() { MealId = meal.MealId, ItemId = selectedItem });
                }
            }
        }

        private async Task UpdateMealItems(List<MealItem> submittedMealItems, Guid mealId)
        {
            var existingMealItems = await mealPlanRepository.GetMealItems(mealId);
            var mealItemsToRemove = existingMealItems.Except(submittedMealItems, new MealItemComparer());

            mealPlanRepository.RemoveMealItems(mealItemsToRemove);

            foreach (var additionalMealItem in submittedMealItems)
            {
                if (!existingMealItems.Contains(additionalMealItem, new MealItemComparer()))
                {
                    mealPlanRepository.AddMealItem(additionalMealItem);
                }
            }
        }

        private void GetSubmittedMealDiners(List<UserMeal> submittedMealDiners, MealViewModel meal)
        {
            if (meal.Diners != null)
            {
                foreach (var diner in meal.Diners)
                {
                    submittedMealDiners.Add(new UserMeal() { MealId = meal.MealId, UserId = diner });
                }
            }
        }

        private async Task CheckIfNewMealRequired(MealViewModel meal, Guid mealPlanId)
        {
            var mealExists = await mealPlanRepository.MealExists(meal.MealId);
            if (!mealExists)
            {
                var newMeal = new Meal()
                {
                    MealId = meal.MealId,
                    Date = meal.Date,
                    MealPlanId = mealPlanId
                };
                mealPlanRepository.AddMeal(newMeal);
            }
        }

        private async Task UpdateMealDiners(List<UserMeal> submittedMealDiners, Guid mealId)
        {
            var exisitingMealDiners = await mealPlanRepository.GetMealDiners(mealId);
            var userMealsToRemove = exisitingMealDiners.Except(submittedMealDiners, new UserMealComparer());

            mealPlanRepository.RemoveUserMeals(userMealsToRemove);

            foreach (var additionalDiner in submittedMealDiners)
            {
                if (!exisitingMealDiners.Contains(additionalDiner, new UserMealComparer()))
                {
                    mealPlanRepository.AddUserMeal(additionalDiner);
                }
            }
        }
    }
}

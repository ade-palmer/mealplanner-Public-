using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealPlanner365.Models;
using MealPlanner365.Models.ViewModels;
using MealPlanner365.Services;
using MealPlanner365.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MealPlanner365.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly IShoppingRepository shoppingRepository;

        public ShoppingController(IShoppingRepository shoppingRepository)
        {
            this.shoppingRepository = shoppingRepository;
        }


        public async Task<IActionResult> Index(Guid mealPlanId)
        {
            var mealPlanExists = await shoppingRepository.MealPlanExists(mealPlanId);

            if (!mealPlanExists)
            {
                return NotFound();
            }

            var shoppingDates = await shoppingRepository.GetShoppingDates(mealPlanId);

            var shoppingViewModel =  new List<ShoppingViewModel>();

            foreach (var shoppingDate in shoppingDates)
            {
                shoppingViewModel.Add(new ShoppingViewModel()
                {
                    ShoppingId = shoppingDate.ShoppingId,
                    Date = shoppingDate.Date,
                    MealPlanId = shoppingDate.MealPlanId
                });
            }

            ViewData["MealPlan"] = mealPlanId;

            return View(shoppingViewModel);
        }


        public IActionResult CreateShopping(Guid mealPlanId)
        {
            var newShoppingDate = new ShoppingViewModel()
            {
                ShoppingId = Guid.NewGuid(),
                Date = DateTimeOffset.UtcNow,
                MealPlanId = mealPlanId,
            };

            ViewData["MealPlan"] = mealPlanId;

            return View(newShoppingDate);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateShopping(ShoppingViewModel shoppingViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(shoppingViewModel);
            }

            if (shoppingViewModel.Date.Date < DateTimeOffset.UtcNow.Date)
            {
                ModelState.AddModelError(string.Empty, "This date is in the past. Please add a valid future date");
                return View(shoppingViewModel);
            }

            if (await shoppingRepository.ShoppingDayExists(shoppingViewModel.Date, shoppingViewModel.MealPlanId))
            {
                ModelState.AddModelError(string.Empty, "There is already a shopping entry for that day");
                return View(shoppingViewModel);
            }

            var shoppingDateToAdd = new Shopping()
            {
                ShoppingId = shoppingViewModel.ShoppingId,
                Date = shoppingViewModel.Date,
                MealPlanId = shoppingViewModel.MealPlanId
            };

            shoppingRepository.AddShopping(shoppingDateToAdd);

            await shoppingRepository.SaveChangesAsync();

            return RedirectToAction(actionName: shoppingViewModel.MealPlanId.ToString(),
                                    controllerName: "Shopping");
        }


        public async Task<IActionResult> EditShopping(Guid shoppingId)
        {
            if (shoppingId == Guid.Empty)
            {
                return NotFound();
            }

            var getShoppingById = await shoppingRepository.GetShoppingById(shoppingId);

            if (getShoppingById == null)
            {
                return NotFound();
            }

            var editShoppingDate = new ShoppingViewModel()
            {
                ShoppingId = getShoppingById.ShoppingId,
                Date = getShoppingById.Date,
                MealPlanId = getShoppingById.MealPlanId,
            };

            ViewData["MealPlan"] = getShoppingById.MealPlanId;

            return View(editShoppingDate);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditShopping(ShoppingViewModel shoppingViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(shoppingViewModel);
            }

            if (shoppingViewModel.Date.Date < DateTimeOffset.UtcNow.Date)
            {
                ModelState.AddModelError(string.Empty, "This date is in the past. Please add a valid future date");
                return View(shoppingViewModel);
            }

            if (await shoppingRepository.ShoppingDayExists(shoppingViewModel.Date, shoppingViewModel.MealPlanId))
            {
                ModelState.AddModelError(string.Empty, "There is already a shopping entry for that day");
                return View(shoppingViewModel);
            }

            var shoppingToUpdate = new Shopping()
            {
                ShoppingId = shoppingViewModel.ShoppingId,
                Date = shoppingViewModel.Date,
                MealPlanId = shoppingViewModel.MealPlanId
            };

            shoppingRepository.UpdateShopping(shoppingToUpdate);

            await shoppingRepository.SaveChangesAsync();

            return RedirectToAction(actionName: shoppingViewModel.MealPlanId.ToString(),
                                controllerName: "Shopping");
        }


        public async Task<IActionResult> DeleteShopping(Guid shoppingId)
        {
            if (shoppingId == Guid.Empty)
            {
                return NotFound();
            }

            var shopping = await shoppingRepository.GetShoppingById(shoppingId);

            if (shopping == null)
            {
                return NotFound();
            }

            var shoppingToDelete = new ShoppingViewModel()
            {
                ShoppingId = shoppingId,
                Date = shopping.Date,
                MealPlanId = shopping.MealPlanId,
            };

            ViewData["MealPlan"] = shopping.MealPlanId;

            return View(shoppingToDelete);
        }


        [HttpPost, ActionName("DeleteShopping")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteShoppingConfirmed(Guid shoppingId)
        {
            var shoppingToDelete = await shoppingRepository.GetShoppingById(shoppingId);

            shoppingRepository.DeleteShopping(shoppingToDelete);

            await shoppingRepository.SaveChangesAsync();

            return RedirectToAction(actionName: shoppingToDelete.MealPlanId.ToString(),
                                controllerName: "Shopping");
        }
    }

}

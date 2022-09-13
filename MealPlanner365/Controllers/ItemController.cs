using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MealPlanner365.Models;
using MealPlanner365.Models.ViewModels;
using MealPlanner365.Services;
using MealPlanner365.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace MealPlanner365.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemRepository itemRepository;
        private readonly ICategoryRepository categoryRepository;

        public ItemController(IItemRepository itemRepository, ICategoryRepository categoryRepository)
        {
            this.itemRepository = itemRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index(Guid mealPlanId)
        {
            var mealPlanExists = await itemRepository.MealPlanExists(mealPlanId);

            if (!mealPlanExists)
            {
                return NotFound();
            }

            IEnumerable<ItemViewModel> items = await itemRepository.GetItems(mealPlanId);

            ViewData["MealPlan"] = mealPlanId;

            return View(items);
        }


        public async Task<IActionResult> CreateItem(Guid mealPlanId)
        {
            var mealPlanExists = await itemRepository.MealPlanExists(mealPlanId);

            if (!mealPlanExists)
            {
                return NotFound();
            }

            var newItem = new ItemViewModel()
            {
                ItemId = Guid.NewGuid(),
                MealPlanId = mealPlanId
            };

            ViewBag.Categories = new SelectList(await categoryRepository.GetCategories(mealPlanId), "FoodTypeId", "Name");

            ViewData["MealPlan"] = mealPlanId;

            return View(newItem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem(ItemViewModel itemViewModel)
        {
            ViewData["MealPlan"] = itemViewModel.MealPlanId;
            ViewBag.Categories = new SelectList(await categoryRepository.GetCategories(itemViewModel.MealPlanId), "FoodTypeId", "Name");

            if (!ModelState.IsValid)
            {
                return View(itemViewModel);
            }

            if (await itemRepository.ItemExists(itemViewModel.ItemName.Trim(), itemViewModel.FoodTypeId, itemViewModel.MealPlanId))
            {
                ModelState.AddModelError(string.Empty, "An Item with that name already exist in this category");
                return View(itemViewModel);
            }

            var itemToAdd = new Item()
            {
                ItemId = itemViewModel.ItemId,
                Name = itemViewModel.ItemName,
                FoodTypeId = itemViewModel.FoodTypeId,
                MealPlanId = itemViewModel.MealPlanId
            };

            itemRepository.AddItem(itemToAdd);

            await itemRepository.SaveChangesAsync();

            return RedirectToAction(actionName: itemViewModel.MealPlanId.ToString(),
                                    controllerName: "Item");
        }


        public async Task<IActionResult> EditItem(Guid ItemId)
        {
            if (ItemId == Guid.Empty)
            {
                return NotFound();
            }

            var getItemById = await itemRepository.GetItemById(ItemId);

            if (getItemById == null)
            {
                return NotFound();
            }

            var itemUsageCount = await itemRepository.GetItemUsage(ItemId);
            if (itemUsageCount > 0)
            {
                TempData["StatusMessage"] = $"Warning: This Item has been used in {itemUsageCount} Meals. Editing or Deleting this Item will update these entries.";
            }

            var editItem = new ItemViewModel()
            {
                ItemId = getItemById.ItemId,
                ItemName = getItemById.Name,
                FoodTypeId = getItemById.FoodTypeId,
                FoodTypeName = getItemById.FoodType.Name,
                MealPlanId = (Guid)getItemById.MealPlanId  // TODO: Used an explicit cast to Guid - Required?
            };

            ViewBag.Categories = new SelectList(await categoryRepository.GetCategories((Guid)getItemById.MealPlanId), "FoodTypeId", "Name"); // TODO: Had to add explicit Guid cast here - REQUIRED?

            ViewData["MealPlan"] = getItemById.MealPlanId;

            return View(editItem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(ItemViewModel itemViewModel)
        {
            ViewData["MealPlan"] = itemViewModel.MealPlanId;
            ViewBag.Categories = new SelectList(await categoryRepository.GetCategories(itemViewModel.MealPlanId), "FoodTypeId", "Name"); // TODO: Had to add explicit Guid cast here - REQUIRED?

            if (!ModelState.IsValid)
            {
                return View(itemViewModel);
            }

            var item = await itemRepository.GetItemById(itemViewModel.ItemId);

            if (!string.Equals(item.Name, itemViewModel.ItemName, StringComparison.OrdinalIgnoreCase))
            {
                if (await itemRepository.ItemExists(itemViewModel.ItemName.Trim(), itemViewModel.FoodTypeId, itemViewModel.MealPlanId))
                {
                    ModelState.AddModelError(string.Empty, "An Item with this name already exists in that Category");
                    return View(itemViewModel);
                }
            }

            var itemToUpdate = new Item()
            {
                ItemId = itemViewModel.ItemId,
                Name = itemViewModel.ItemName,
                FoodTypeId = itemViewModel.FoodTypeId,
                MealPlanId = itemViewModel.MealPlanId
            };

            itemRepository.UpdateItem(itemToUpdate);

            await itemRepository.SaveChangesAsync();

            return RedirectToAction(actionName: itemViewModel.MealPlanId.ToString(),
                                    controllerName: "Item");
        }


        public async Task<IActionResult> DeleteItem(Guid itemId)
        {
            if (itemId == Guid.Empty)
            {
                return NotFound();
            }

            var item = await itemRepository.GetItemById(itemId);

            if (item == null)
            {
                return NotFound();
            }

            var itemUsageCount = await itemRepository.GetItemUsage(itemId);
            if (itemUsageCount > 0)
            {
                TempData["StatusMessage"] = $"Warning: This Item has been used in {itemUsageCount} Meals. Editing or Deleting this Item will update these entries.";
            }

            var itemToDelete = new ItemViewModel()
            {
                ItemId = item.ItemId,
                ItemName = item.Name,
                FoodTypeId = item.FoodTypeId,
                FoodTypeName = item.FoodType.Name,
                MealPlanId = (Guid)item.MealPlanId  // TODO: Used an explicit Guid - Required?
            };

            ViewBag.Categories = new SelectList(await categoryRepository.GetCategories((Guid)item.MealPlanId), "FoodTypeId", "Name"); // TODO: Had to add explicit Guid cast here - REQUIRED?

            ViewData["MealPlan"] = item.MealPlanId;

            return View(itemToDelete);
        }


        [HttpPost, ActionName("DeleteItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteItemConfirmed(Guid itemId) 
        {
            var itemToDelete = await itemRepository.GetItemById(itemId);

            itemRepository.DeleteItem(itemToDelete);

            // TODO: Check cascading

            await itemRepository.SaveChangesAsync();

            return RedirectToAction(actionName: itemToDelete.MealPlanId.ToString(),
                                    controllerName: "Item");
        }
    }
}

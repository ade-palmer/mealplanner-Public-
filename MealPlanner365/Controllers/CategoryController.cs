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
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }


        public async Task<IActionResult> Index(Guid mealPlanId)
        {
            var mealPlanExists = await categoryRepository.MealPlanExists(mealPlanId);

            if (!mealPlanExists)
            {
                return NotFound();
            }

            var categories = await categoryRepository.GetCategories(mealPlanId);

            var categoryViewModel = new List<CategoryViewModel>();

            foreach (var category in categories)
            {
                categoryViewModel.Add(new CategoryViewModel()
                {
                    CategoryId = category.FoodTypeId,
                    Name = category.Name,
                });
            }

            ViewData["MealPlan"] = mealPlanId;

            return View(categoryViewModel);
        }


        public async Task<IActionResult> CreateCategory(Guid mealPlanId)
        {
            var mealPlanExists = await categoryRepository.MealPlanExists(mealPlanId);

            if (!mealPlanExists)
            {
                return NotFound();
            }

            var newCategory = new CategoryViewModel()
            {
                CategoryId = Guid.NewGuid(),
                MealPlanId = mealPlanId
            };

            ViewData["MealPlan"] = mealPlanId;

            return View(newCategory);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryViewModel categoryViewModel)
        {
            ViewData["MealPlan"] = categoryViewModel.MealPlanId;

            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }

            if (await categoryRepository.CategoryExists(categoryViewModel.Name.Trim(), categoryViewModel.MealPlanId))
            {
                ModelState.AddModelError(string.Empty, "A Category with this name already exists");
                return View(categoryViewModel);
            }

            var categoryToAdd = new FoodType()
            {
                FoodTypeId = categoryViewModel.CategoryId,
                Name = categoryViewModel.Name.Trim(),
                MealPlanId = categoryViewModel.MealPlanId
            };

            categoryRepository.AddCategory(categoryToAdd);

            await categoryRepository.SaveChangesAsync();

            return RedirectToAction(actionName: categoryViewModel.MealPlanId.ToString(),
                                    controllerName: "Category");
        }


        public async Task<IActionResult> EditCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return NotFound();
            }

            var getCategoryById = await categoryRepository.GetCategoryById(categoryId);

            if (getCategoryById == null)
            {
                return NotFound();
            }

            // TODO: Move to shared class
            var itemUsageCount = await categoryRepository.GetCategoriesIncludingItemsCount(categoryId);
            if (itemUsageCount > 0)
            {
                var mealUsageCount = await categoryRepository.GetCategoriesIncludingMealCount(categoryId);
                TempData["StatusMessage"] = $"Warning: This Category has been assigned to {itemUsageCount} Items and {mealUsageCount} Meals. Editing or Deleting this Category will update these entries.";
            }

            var editCategory = new CategoryViewModel()
            {
                CategoryId = getCategoryById.FoodTypeId,
                Name = getCategoryById.Name,
                MealPlanId = getCategoryById.MealPlanId
            };

            ViewData["MealPlan"] = getCategoryById.MealPlanId;

            return View(editCategory);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(CategoryViewModel categoryViewModel)
        {
            ViewData["MealPlan"] = categoryViewModel.MealPlanId;

            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }

            var category = await categoryRepository.GetCategoryById(categoryViewModel.CategoryId);

            if (!string.Equals(category.Name, categoryViewModel.Name, StringComparison.OrdinalIgnoreCase))
            {
                if (await categoryRepository.CategoryExists(categoryViewModel.Name.Trim(), categoryViewModel.MealPlanId))
                {
                    ModelState.AddModelError(string.Empty, "A Category with this name already exists");
                    return View(categoryViewModel);
                }
            }

            var categoryToUpdate = new FoodType()
            {
                FoodTypeId = categoryViewModel.CategoryId,
                Name = categoryViewModel.Name.Trim(),
                MealPlanId = categoryViewModel.MealPlanId
            };

            categoryRepository.UpdateCategory(categoryToUpdate);

            await categoryRepository.SaveChangesAsync();

            return RedirectToAction(actionName: categoryViewModel.MealPlanId.ToString(),
                                controllerName: "Category");
        }


        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                return NotFound();
            }

            var category = await categoryRepository.GetCategoryById(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            ViewData["MealPlan"] = category.MealPlanId;

            // TODO: Move to shared class
            var itemUsageCount = await categoryRepository.GetCategoriesIncludingItemsCount(categoryId);
            if (itemUsageCount > 0)
            {
                var mealUsageCount = await categoryRepository.GetCategoriesIncludingMealCount(categoryId);
                TempData["StatusMessage"] = $"Warning: This Category has been assigned to {itemUsageCount} Items and {mealUsageCount} Meals. Editing or Deleting this Category will update these entries.";
            }

            var categoryToDelete = new CategoryViewModel()
            {
                CategoryId = categoryId,
                Name = category.Name,
                MealPlanId = category.MealPlanId
            };

            return View(categoryToDelete);
        }


        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryConfirmed(Guid categoryId)
        {
            var categoryToDelete = await categoryRepository.GetCategoryById(categoryId);

            categoryRepository.DeleteCategory(categoryToDelete);

            // TODO: Check if Cascade delete working

            await categoryRepository.SaveChangesAsync();

            return RedirectToAction(actionName: categoryToDelete.MealPlanId.ToString(),
                                controllerName: "Category");
        }
    }
}

using MealPlanner365.Contexts;
using MealPlanner365.Models;
using MealPlanner365.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Services
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
        }

        public void AddCategory(FoodType category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            dbContext.Add(category);
        }

        public async Task<bool> CategoryExists(string name, Guid mealPlanId)
        {
            var categoryExists = await dbContext.FoodTypes
                .AsNoTracking()
                .AnyAsync(n => n.Name.ToLower() == name.ToLower() && 
                               n.MealPlanId == mealPlanId);

            return categoryExists;
        }

        public void DeleteCategory(FoodType categoryToDelete)
        {
            if (categoryToDelete == null)
            {
                throw new ArgumentNullException(nameof(categoryToDelete));
            }

            dbContext.Remove(categoryToDelete);
        }

        public async Task<IEnumerable<FoodType>> GetCategories(Guid mealPlanId)
        {
            var categories = await dbContext.FoodTypes
                .Where(f => f.MealPlanId == mealPlanId)
                .OrderBy(o => o.Name)
                .ToListAsync();

            return categories;
        }

        public async Task<FoodType> GetCategoryById(Guid categoryId)
        {
            var category = await dbContext.FoodTypes
                .AsNoTracking()
                .SingleOrDefaultAsync(i => i.FoodTypeId == categoryId);

            return category;
        }


        public void UpdateCategory(FoodType categoryToUpdate)
        {
            if (categoryToUpdate == null)
            {
                throw new ArgumentNullException(nameof(categoryToUpdate));
            }

            dbContext.Update(categoryToUpdate);
        }

        public async Task<int> GetCategoriesIncludingItemsCount(Guid categoryId)
        {
            var categoriesIncludingItemsCount = await dbContext.FoodTypes
                .Include(i => i.Items)
                .Where(c => c.FoodTypeId == categoryId)
                .SelectMany(s => s.Items)
                .CountAsync();

            return categoriesIncludingItemsCount;
        }

        public async Task<int> GetCategoriesIncludingMealCount(Guid categoryId)
        {
            var categoriesIncludingMealCount = await dbContext.FoodTypes
                .Include(i => i.Items)
                    .ThenInclude(mi => mi.MealItems)
                .Where(c => c.FoodTypeId == categoryId)
                .SelectMany(z => z.Items).SelectMany(z => z.MealItems).CountAsync();

            return categoriesIncludingMealCount;
        }
    }
}

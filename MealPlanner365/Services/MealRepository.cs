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
    public class MealRepository : BaseRepository, IMealRepository
    {
        public MealRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void AddMeal(Meal meal)
        {
            if (meal == null)
            {
                throw new ArgumentNullException(nameof(meal));
            }

            dbContext.Add(meal);
        }

        public void DeleteMeal(Meal meal)
        {
            if (meal == null)
            {
                throw new ArgumentNullException(nameof(meal));
            }

            dbContext.Remove(meal);
        }

        public async Task<IEnumerable<Meal>> GetFutureMeals(Guid mealPlanId)
        {
            var meals = await dbContext.Meals
                .Include(mi => mi.MealItems)
                    .ThenInclude(i => i.Item)
                .Where(p => p.MealPlanId == mealPlanId)
                .Where(d => d.Date.Date >= DateTimeOffset.UtcNow.Date)
                .OrderBy(d => d.Date)
                .ToListAsync();

            return meals;
        }

        public async Task<Meal> GetMealById(Guid mealId)
        {
            var meal = await dbContext.Meals
                .AsNoTracking()
                .SingleOrDefaultAsync(i => i.MealId == mealId);

            return meal;
        }

        public async Task<Meal> GetMealWithItemsById(Guid mealId)
        {
            var mealWithItems = await dbContext.Meals
                .Include(mi => mi.MealItems)
                    .ThenInclude(i => i.Item)
                .SingleOrDefaultAsync(m => m.MealId == mealId);

            return mealWithItems;
        }
    }
}

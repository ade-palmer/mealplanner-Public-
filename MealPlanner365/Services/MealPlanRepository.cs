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
    public class MealPlanRepository : BaseRepository, IMealPlanRepository
    {
        public MealPlanRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void AddMeal(Meal meal)
        {
            dbContext.Meals.Add(meal);
        }

        public void AddMealItem(MealItem mealItem)
        {
            dbContext.MealItems.Add(mealItem);
        }

        public void AddUserMeal(UserMeal userMeal)
        {
            dbContext.UserMeals.Add(userMeal);
        }

        public async Task<IEnumerable<UserMeal>> GetMealDiners(Guid mealId)
        {
            var mealDiners = await dbContext.UserMeals
                .AsNoTracking()
                .Where(m => m.MealId == mealId)
                .ToListAsync();

            return mealDiners;
        }

        public async Task<IEnumerable<MealItem>> GetMealItems(Guid mealId)
        {
            var mealItems = await dbContext.MealItems
                .AsNoTracking()
                .Where(m => m.MealId == mealId)
                .ToListAsync();

            return mealItems;
        }

        public async Task<IEnumerable<ApplicationUser>> GetMealPlanDiners(Guid mealPlanId)
        {
            var mealPlanDiners = await dbContext.Users
                .AsNoTracking()
                .Where(p => p.MealPlanId == mealPlanId)
                .OrderBy(u => u.UserName)
                .ToListAsync();

            return mealPlanDiners;
        }

        public async Task<DayOfWeek> GetMealPlanWeekStartDay(Guid mealPlanId)
        {
            var mealPlanWeekStartDay = await dbContext.MealPlans
                .AsNoTracking()
                .Where(p => p.MealPlanId == mealPlanId)
                .Select(d => d.WeekStartDay)
                .SingleOrDefaultAsync();

            return mealPlanWeekStartDay;    
        }

        public async Task<IEnumerable<Meal>> GetMeals(Guid mealPlanGuid, DateTimeOffset firstDayOfWeek, int totalDays)
        {
            var meals = await dbContext.Meals
                .Include(u => u.UserMeals)
                .Include(i => i.MealItems)
                .Where(p => p.MealPlanId == mealPlanGuid)
                .Where(d => d.Date >= firstDayOfWeek &&
                            d.Date <= firstDayOfWeek.AddDays(totalDays - 1))
                .OrderBy(d => d.Date)
                .ToListAsync();

            return meals;
        }

        public async Task<bool> MealExists(Guid mealId)
        {
            return (await dbContext.Meals.AsNoTracking().AnyAsync(m => m.MealId == mealId));
        }

        public void RemoveMealItems(IEnumerable<MealItem> mealItems)
        {
            dbContext.MealItems.RemoveRange(mealItems);  // TODO: Should this be Async. If so, calling method also needs to be changed to await
        }

        public void RemoveUserMeals(IEnumerable<UserMeal> userMeals)
        {
            dbContext.UserMeals.RemoveRange(userMeals);  // TODO: Should this be Async. If so, calling method also needs to be changed to await
        }





        //public async Task<IEnumerable<MealItemsViewModel>> GetMealItems(Guid mealPlanId)
        //{
        //    var mealItems = await dbContext.MealItems
        //        .Where(t => t.Meal.MealPlanId == mealPlanId)
        //        .Include(i => i.Item)
        //            .ThenInclude(f => f.FoodType)
        //        .Select(f => new MealItemsViewModel
        //        {
        //            ItemId = f.Item.ItemId,
        //            ItemName = f.Item.Name,
        //            //FoodTypeId = f.Item.FoodTypeId,
        //            FoodTypeName = f.Item.FoodType.Name
        //        })
        //        .Distinct()
        //        .OrderBy(o => o.FoodTypeName)
        //            .ThenBy(o => o.ItemName)
        //        .ToListAsync();

        //    return mealItems;
        //}

        //public async Task<IEnumerable<Meal>> GetMeals(Guid mealPlanId)
        //{
        //    var meals = await dbContext.Meals
        //        .Where(m => m.MealPlanId == mealPlanId)
        //        .Where(d => d.Date.Date >= DateTimeOffset.UtcNow.Date)
        //        .OrderBy(o => o.Date)
        //        .ToListAsync();

        //    return meals;
        //}


    }
}

using MealPlanner365.Models;
using MealPlanner365.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Services.Interfaces
{
    public interface IMealPlanRepository : IBaseRepository
    {
        Task<DayOfWeek> GetMealPlanWeekStartDay(Guid mealPlanId);
        Task<IEnumerable<Meal>> GetMeals(Guid mealPlanGuid, DateTimeOffset firstDayOfWeek, int totalDays);
        Task<IEnumerable<ApplicationUser>> GetMealPlanDiners(Guid mealPlanId);
        Task<bool> MealExists(Guid mealId);
        void AddMeal(Meal meal);
        Task<IEnumerable<MealItem>> GetMealItems(Guid mealId);
        void RemoveMealItems(IEnumerable<MealItem> mealItems);
        void AddMealItem(MealItem mealItem);
        Task<IEnumerable<UserMeal>> GetMealDiners(Guid mealId);
        void RemoveUserMeals(IEnumerable<UserMeal> userMeals);
        void AddUserMeal(UserMeal us);

        //Task<IEnumerable<MealItemsViewModel>> GetMealItems(Guid mealPlanId);

        //Task<IEnumerable<Shopping>> GetShoppingDates(Guid mealPlanId);
        //Task<IEnumerable<Meal>> GetMeals(Guid mealPlanId);

    }
}

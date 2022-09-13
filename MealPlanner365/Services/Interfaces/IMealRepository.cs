using MealPlanner365.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Services.Interfaces
{
    public interface IMealRepository : IBaseRepository
    {
        Task<Meal> GetMealById(Guid mealId);
        Task<Meal> GetMealWithItemsById(Guid mealId);
        Task<IEnumerable<Meal>> GetFutureMeals(Guid mealPlanId);
        void AddMeal(Meal meal);
        void DeleteMeal(Meal meal);
    }
}

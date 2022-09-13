using MealPlanner365.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Services.Interfaces
{
    public interface IShoppingRepository : IBaseRepository
    {
        Task<IEnumerable<Shopping>> GetShoppingDates(Guid mealPlanId);
        Task<bool> ShoppingDayExists(DateTimeOffset date, Guid mealPlanId);
        void AddShopping(Shopping shoppingDate);
        Task<Shopping> GetShoppingById(Guid shoppingId);
        Task<IEnumerable<Shopping>> GetShoppingByDate(Guid mealPlanId, DateTimeOffset firstDayOfWeek, int totalDays);
        void UpdateShopping(Shopping shoppingToUpdate);
        void DeleteShopping(Shopping shoppingToDelete);
    }
}

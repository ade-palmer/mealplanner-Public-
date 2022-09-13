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
    public class ShoppingRepository : BaseRepository, IShoppingRepository
    {
        public ShoppingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Shopping>> GetShoppingDates(Guid mealPlanId)
        {
            var shoppingDates = await dbContext.Shopping
                .Where(m => m.MealPlanId == mealPlanId)
                .Where(d => d.Date.Date >= DateTimeOffset.UtcNow.Date)
                .OrderBy(o => o.Date)
                .ToListAsync();

            return shoppingDates;
        }

        public async Task<bool> ShoppingDayExists(DateTimeOffset date, Guid mealPlanId)
        {
            return (await dbContext.Shopping.AsNoTracking().AnyAsync(d => d.Date == date && d.MealPlanId == mealPlanId));
        }

        public void AddShopping(Shopping shoppingDate)
        {
            if (shoppingDate == null)
            {
                throw new ArgumentNullException(nameof(shoppingDate));
            }

            dbContext.Add(shoppingDate);
        }

        public async Task<Shopping> GetShoppingById(Guid shoppingId)
        {
            var shopping = await dbContext.Shopping
                .AsNoTracking()
                .SingleOrDefaultAsync(i => i.ShoppingId == shoppingId);

            return shopping;
        }

        public void UpdateShopping(Shopping shoppingToUpdate)
        {
            if (shoppingToUpdate == null)
            {
                throw new ArgumentNullException(nameof(shoppingToUpdate));
            }

            dbContext.Update(shoppingToUpdate);
        }

        public void DeleteShopping(Shopping shoppingToDelete)
        {
            if (shoppingToDelete == null)
            {
                throw new ArgumentNullException(nameof(shoppingToDelete));
            }

            dbContext.Remove(shoppingToDelete);
        }

        public async Task<IEnumerable<Shopping>> GetShoppingByDate(Guid mealPlanId, DateTimeOffset firstDayOfWeek, int totalDays)
        {
            var shoppingDays = await dbContext.Shopping
                .Where(p => p.MealPlanId == mealPlanId)
                .Where(d => d.Date >= firstDayOfWeek &&
                            d.Date <= firstDayOfWeek.AddDays(totalDays - 1))
                .ToListAsync();

            return shoppingDays;
        }
    }
}
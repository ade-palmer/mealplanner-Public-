using MealPlanner365.Contexts;
using MealPlanner365.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Services
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly ApplicationDbContext dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public async Task<bool> MealPlanExists(Guid mealPlanId)
        {
            var mealPlanExists = await dbContext.MealPlans
                .AnyAsync(o => o.MealPlanId == mealPlanId);

            return mealPlanExists;
        }


        public async Task<bool> SaveChangesAsync()
        {
            return (await dbContext.SaveChangesAsync() > 0);
        }
    }
}

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
    public class SettingsRepository : BaseRepository, ISettingsRepository
    {
        public SettingsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
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

        public async Task<bool> IsAdministrator(string UserId)
        {
            const string RoleName = "Administrator";

            var isAdmin = await (from u in dbContext.UserRoles
                                 join r in dbContext.Roles
                                     on u.RoleId equals r.Id
                                 where u.UserId == UserId
                                 where r.Name == RoleName
                                 select r).AnyAsync();

            return isAdmin;
        }
    }
}

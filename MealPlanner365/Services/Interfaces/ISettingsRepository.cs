using MealPlanner365.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Services.Interfaces
{
    public interface ISettingsRepository : IBaseRepository
    {
        Task<IEnumerable<ApplicationUser>> GetMealPlanDiners(Guid mealPlanId);
        Task<bool> IsAdministrator(string UserId);
    }
}

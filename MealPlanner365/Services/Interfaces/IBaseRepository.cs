using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Services.Interfaces
{
    public interface IBaseRepository
    {
        Task<bool> MealPlanExists(Guid mealPlanId);

        Task<bool> SaveChangesAsync();
    }
}

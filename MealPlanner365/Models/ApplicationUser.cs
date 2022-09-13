using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTimeOffset EnrollmentDateTime { get; set; }

        public Guid? MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }

        public ICollection<UserMeal> UserMeals { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models
{
    [Table("Meals")]
    public class Meal
    {
        [Key]
        public Guid MealId { get; set; }

        public DateTimeOffset Date { get; set; }

        public Guid MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }

        public ICollection<MealItem> MealItems { get; set; }

        public ICollection<UserMeal> UserMeals { get; set; }
    }
}

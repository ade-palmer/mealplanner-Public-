using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models
{
    public class Shopping
    {
        [Key]
        public Guid ShoppingId { get; set; }
        public DateTimeOffset Date { get; set; }

        public Guid MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models
{
    [Table("UserMeals")]
    public class UserMeal
    {
        [StringLength(450)]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid MealId { get; set; }
        public Meal Meal { get; set; }
    }
}

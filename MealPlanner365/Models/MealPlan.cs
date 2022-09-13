using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models
{
    [Table("MealPlans")]
    public class MealPlan
    {
        [Key]
        public Guid MealPlanId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        public DayOfWeek WeekStartDay { get; set; }

        public ICollection<ApplicationUser> User { get; set; }

        public ICollection<Meal> Meals { get; set; }

        public ICollection<Item> Items { get; set; }

        public ICollection<FoodType> FoodTypes { get; set; }

        public ICollection<Shopping> Shopping { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models
{
    [Table("FoodTypes")]
    public class FoodType
    {
        [Key]
        public Guid FoodTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public Guid MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}

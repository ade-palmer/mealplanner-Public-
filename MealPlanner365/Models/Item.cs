using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models
{
    [Table("Items")]
    public class Item
    {
        [Key]
        public Guid ItemId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public Guid? FoodTypeId { get; set; }
        public FoodType FoodType { get; set; }

        public Guid? MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }

        public ICollection<MealItem> MealItems { get; set; }
    }
}

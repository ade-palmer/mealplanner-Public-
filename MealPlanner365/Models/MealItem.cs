using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models
{
    [Table("MealItems")]
    public class MealItem
    {
        public Guid MealId { get; set; }
        public Meal Meal { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models.ViewModels
{
    public class MealItemsViewModel
    {
        public Guid ItemId { get; set; }
        public string ItemName { get; set; }
        public Guid FoodTypeId { get; set; }
        public string FoodTypeName { get; set; }
    }
}

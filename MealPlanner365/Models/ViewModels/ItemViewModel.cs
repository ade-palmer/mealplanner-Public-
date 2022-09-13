using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models.ViewModels
{
    public class ItemViewModel : AdminViewModel
    {
        public Guid ItemId { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Item name", Prompt = "Enter item name here")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Guid? FoodTypeId { get; set; }
        public string FoodTypeName { get; set; }
    }
}

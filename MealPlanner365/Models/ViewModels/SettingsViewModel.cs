using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models.ViewModels
{
    public class SettingsViewModel
    {
        [Display(Name = "Meal Plan URL:")]
        public string Url { get; set; }

        [Display(Name = "Days to display:")]
        public int DisplayDays { get; set; }

        [Display(Name = "Page Increments:")]
        public int PageIncrements { get; set; }

        public ICollection<UserViewModel> Diners { get; set; }
    }
}

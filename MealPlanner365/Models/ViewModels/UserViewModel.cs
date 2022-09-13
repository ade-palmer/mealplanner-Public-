using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models.ViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [Display(Name = "Confirmed")]
        public bool EmailConfirmed { get; set; }
        public bool LockedOut { get; set; }
        public bool Administrator { get; set; }
        public Guid MealPlanId { get; set; }
    }
}

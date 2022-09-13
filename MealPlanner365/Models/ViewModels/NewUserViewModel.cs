using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models.ViewModels
{
    public class NewUserViewModel
    {
        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Name", Prompt = "Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address", Prompt = "name@address.com")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password", Prompt = "Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public DateTimeOffset EnrollmentDateTime { get; set; } = DateTime.Now;   
        public bool Administrator { get; set; }

        [Display(Name = "Meal Plan Id")]
        public Guid MealPlanId { get; set; }
    }
}

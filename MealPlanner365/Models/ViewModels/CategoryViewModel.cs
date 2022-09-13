using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models.ViewModels
{
    public class CategoryViewModel : AdminViewModel
    {
        public Guid CategoryId { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Name { get; set; }
    }
}

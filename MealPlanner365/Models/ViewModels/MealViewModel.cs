using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models.ViewModels
{
    public class MealViewModel
    {
        public Guid MealId { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:ddd}")]
        public DateTimeOffset Date { get; set; }

        //public Guid MealPlanId { get; set; }
        public bool DisplayDate { get; set; }

        public bool ShoppingDay { get; set; }

        public IEnumerable<Guid> SelectedItems { get; set; } // TODO: This was a String. Is Guid OK

        public IEnumerable<String> Diners { get; set; }
    }
}

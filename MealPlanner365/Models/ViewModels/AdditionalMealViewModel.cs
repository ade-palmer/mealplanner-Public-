using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models.ViewModels
{
    public class AdditionalMealViewModel : AdminViewModel
    {
        public Guid MealId { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset Date { get; set; }

        public IEnumerable<string> Items { get; set; }
    }
}

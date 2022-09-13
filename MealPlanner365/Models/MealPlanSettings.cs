using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models
{
    public class MealPlanSettings
    {
        public string Url { get; set; }
        public int DisplayDays { get; set; }
        public int PageIncrements { get; set; }
    }
}

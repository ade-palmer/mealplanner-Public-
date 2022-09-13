using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.Models.ViewModels
{
    public class MealPlanViewModel
    {
        [HiddenInput]
        public int PageOffset { get; set; }

        [HiddenInput]
        public Guid MealPlanId { get; set; }

        public List<MealViewModel> Meals { get; set; }  //TODO: This was a List. Is ICollecetion OK - NO Because couldnt iterate through in view?
    }
}

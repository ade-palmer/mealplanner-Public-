using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MealPlanner365.Models.ViewModels
{
    public class AdminViewModel
    {
        public Guid MealPlanId { get; set; }
        public AdminTabs SelectedTab { get; set; } = AdminTabs.Items;
    }

    public enum AdminTabs
    {
        Items,
        Categories,
        Meals,
        Shopping
    }
}

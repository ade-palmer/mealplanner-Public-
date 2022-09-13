using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanner365.TagHelpers
{
    [HtmlTargetElement("date-display")]
    public class DateDisplayTagHelper : TagHelper
    {
        public DateTimeOffset Date { get; set; }
        public bool Show { get; set; }
        public bool IsShoppingDay { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Show)
            {
                if (IsShoppingDay)
                {
                    output.TagName = "i";
                    output.Attributes.SetAttribute("class", "fas fa-shopping-basket fa-2x text-info");
                }
                else
                {
                    
                    var badgeColour = (Date == DateTimeOffset.UtcNow.Date) ? "info" : "secondary";
                    output.TagName = "span";
                    output.Attributes.SetAttribute("class", $@"badge badge-{badgeColour}");
                    output.Attributes.SetAttribute("style", "width: 35px");
                    output.Content.SetHtmlContent(Date.ToString("ddd"));
                }
            }
            else
            {
                output.SuppressOutput();
            }
        }
    }
}

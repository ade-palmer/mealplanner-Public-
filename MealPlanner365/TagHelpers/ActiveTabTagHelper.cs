using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MealPlanner365.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "active-url")]
    //[HtmlTargetElement("a", Attributes = nameof(ActiveUrl))]  // TODO: This worked for other ViewComponent
    public class ActiveTabTagHelper : TagHelper
    {
        public string ActiveUrl { get; set; }

        private readonly IHttpContextAccessor httpContextAccessor;
        public ActiveTabTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (httpContextAccessor.HttpContext.Request.Path.ToString().Contains(ActiveUrl))
            {
                output.Attributes.SetAttribute("class", "nav-link text-dark active");
                output.Attributes.SetAttribute("aria-selected", "true");
            }
        }
    }
}

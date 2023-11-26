using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace NewsAggregatingProject.MVC7.TagHelpers
{
    public class WatchTagHelper:TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Content.SetContent($"Current time: {DateTime.Now:R}");
        }
    }
}

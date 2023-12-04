using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NewsAggregatingProject.TagHelpers
{
    public class WatchTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Content.SetContent($"Current time: {DateTime.Now:R}");
        }
    }
}

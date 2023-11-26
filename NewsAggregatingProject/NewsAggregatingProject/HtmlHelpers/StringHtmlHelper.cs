using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System.Text;

namespace NewsAggregatingProject.MVC7.HtmlHelpers
{
    public static class StringHtmlHelper
    {
        public static HtmlString CreateList(this IHtmlHelper htmlHelper, string[] items)
        {
            var result = new StringBuilder("<ul>");
            foreach( var item in items )
            {
                result.Append($"<li>{item}<li>");
            }
            result.Append("<ul>");
            return new HtmlString(result.ToString());
        }
    }
}

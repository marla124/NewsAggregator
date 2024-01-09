using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.RegularExpressions;

namespace NewsAggregatingProject.Filters
{
    public class UserAgentCheckerFilter : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            
            var userAgent = context.HttpContext.Request.Headers["UserAgent"].ToString();
            if(Regex.IsMatch(userAgent, "Googlebot/2.1"))
            {
                context.Result = new ContentResult() { Content = "No google bots allowed" };
            }
            else
            {
                await next();
            }
        }
    }
}

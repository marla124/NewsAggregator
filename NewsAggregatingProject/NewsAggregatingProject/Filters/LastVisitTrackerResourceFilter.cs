using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NewsAggregatingProject.Filters
{
    public class LastVisitTrackerResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Response.Cookies.Append("LastVisitTime", DateTime.Now.ToString("R"));

            context.Result = new ContentResult() { Content = "12345" };
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using System.Text.RegularExpressions;

namespace NewsAggregatingProject.Filters
{
    public class TestActionFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.ActionArguments.ContainsKey("data"))
            {
                context.ActionArguments["data"] = 15;
            }
            await next();
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;

namespace NewsAggregatingProject.Filters
{
    public class TestResourceFilter : Attribute, IResourceFilter
    {
        private int x;
        public string Token;

        public TestResourceFilter(int x, string token)
        {
            this.x = x;
            Token = token;
        }
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("x", x.ToString());
            context.HttpContext.Response.Headers.Add("Token", Token.ToString());

        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}

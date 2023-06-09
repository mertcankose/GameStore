using GameStore.Models;

namespace GameStore.Middlewares
{
    public class TestMiddleware
    {
        private RequestDelegate nextDelegate;

        public TestMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }

        public async Task Invoke(HttpContext httpContext, UserContext userContext)
        {
            if(httpContext.Request.Path == "/test")
            {
                await httpContext.Response.WriteAsync($"There are {userContext.Users.Count()} users\n");
            }
            else
            {
                await nextDelegate(httpContext);
            }
        }
    }
}

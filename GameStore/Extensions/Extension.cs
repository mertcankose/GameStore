using GameStore.Middlewares;

namespace GameStore.Extensions
{
    static public class Extension
    {
        public static IApplicationBuilder UseTest(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<TestMiddleware>();
        }
    }
}

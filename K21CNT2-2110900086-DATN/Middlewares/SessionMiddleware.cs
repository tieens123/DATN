using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace K21CNT2_2110900086_DATN.Middlewares
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Kiểm tra nếu người dùng chưa đăng nhập và không phải trang đăng nhập
            if (context.Session.GetInt32("UserId") == null &&
                !context.Request.Path.StartsWithSegments("/Account/Login"))
            {
                context.Response.Redirect("/Account/Login");
                return;
            }

            await _next(context);
        }
    }

    public static class SessionMiddlewareExtensions
    {
        public static IApplicationBuilder UseSessionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SessionMiddleware>();
        }
    }
}
using Microsoft.AspNetCore.Http;
using Stone.Application.Resources;
using System.Net;
using System.Threading.Tasks;

namespace Stone.Middleware
{
    public class RouteMiddleware : MiddlewareValidation
    {
        private readonly RequestDelegate next;

        public RouteMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await next.Invoke(context);

            if (context.Response.StatusCode == 404)
                await WriteValidationErrorResponse(context, HttpStatusCode.NotFound, new
                {
                    Message = StoneApplicationResources.RouteNotFound
                });
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Stone.Middleware
{
    public class ExceptionMiddleware : MiddlewareValidation
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch (Exception ex)
            {
                await WriteValidationErrorResponse(context, HttpStatusCode.InternalServerError, new
                {
                    Message = ex.Message
                });
            }
        }
    }
}

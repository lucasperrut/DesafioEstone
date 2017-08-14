using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Stone.Middleware
{
    public abstract class MiddlewareValidation
    {
        protected async Task WriteValidationErrorResponse(HttpContext context, HttpStatusCode code, params object[] result)
        {
            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
            var response = new MemoryStream(bytes);
            await response.CopyToAsync(context.Response.Body);
        }
    }
}

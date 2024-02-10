using System.Net;
using System.Text.Json;

namespace LemmeProject.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var msg = new { Message = ex.Message, StatusCode = response.StatusCode, Type = ex.Source };
                var result = JsonSerializer.Serialize(msg);
                await response.WriteAsync(result);
            }
        }
    }
}

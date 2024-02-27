using LemmeProject.Application.Services.Abstract;
using LemmeProject.Application.Services.Concrete;
using LemmeProject.Domain.Entities;
using LemmeProject.Domain.Interfaces;
using System.Net;
using System.Text.Json;

namespace LemmeProject.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private IApplicationErrorRepository _applicationErrorRepository;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IApplicationErrorRepository applicationErrorRepository)
        {
            try
            {
                _applicationErrorRepository = applicationErrorRepository;
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var msg = new { Message = ex.Message, StatusCode = response.StatusCode, Type = ex.Source };
                var result = JsonSerializer.Serialize(msg);

                await _applicationErrorRepository.CreateAsync(new ApplicationError() { ErrorMessage = ex.Message, ErrorSource = ex.StackTrace });
                await response.WriteAsync(result);
            }
        }
    }
}

using FluentValidation;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException exception)
            {
                await HandleValidationException(context,exception);
            }
            catch (Exception exception)
            {
                await HandleGeneralException(context, exception);
            }
        }
        private async Task HandleValidationException(HttpContext context, ValidationException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            var response = new
            {
                success = false,
                message = "Validation Failed",
                errors = exception.Errors.Select(e => e.ErrorMessage)
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private async Task HandleGeneralException(HttpContext context, Exception exception)
        {
            //log
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                success = false,
                message = "An unexpected error occured",
                errors = new[] { exception.Message }
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }


    }
}

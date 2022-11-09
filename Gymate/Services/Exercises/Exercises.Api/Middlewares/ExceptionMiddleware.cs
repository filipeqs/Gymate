using Exercises.Api.Errors;
using Exercises.Api.Extensions;
using System.Net;
using System.Text.Json;

namespace Exercises.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(
            ILogger<ExceptionMiddleware> logger, 
            RequestDelegate next, 
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _env.IsDevelopment() || _env.IsLocal()
                ? new ErrorDetails((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                : new ErrorDetails((int)HttpStatusCode.InternalServerError);

            var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, option);

            await context.Response.WriteAsync(json);
        }
    }
}

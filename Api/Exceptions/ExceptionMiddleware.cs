using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SurveyWS.Domain.Exceptions;

namespace SurveyWS.Api.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en solicitud HTTP: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string message;

            if (exception is RequiredValueException or EntityNotFoundException)
            {
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                message = exception.Message;
            }
            else
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                message = "Ha ocurrido un error interno.";
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
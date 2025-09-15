using B1TestTask2.Services.Exceptions;
using System.Net;

namespace B1TestTask2.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ItemNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.NotFound, ex.Message);
            }
            catch (ItemAlreadyExistsException ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex.GetType().Name + " " + ex.Message, HttpStatusCode.InternalServerError, ex.GetType().Name + " " + ex.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string logMessage, HttpStatusCode statusCode, string message)
        {
            logger.LogError(logMessage);

            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            await response.WriteAsJsonAsync(new
            {
                ErrorMesage = message
            });
        }
    }
}

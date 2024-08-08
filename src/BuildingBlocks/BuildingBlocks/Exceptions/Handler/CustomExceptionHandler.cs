
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler
{
    //custom error handling if it is not registed globally
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {

            logger.LogError($"Error Message :{exception.Message} , " +
                $"Time of occurence {DateTime.UtcNow}");

            //pattern matching with anonmous obj


            (string Details, string Titile, int StatusCode) details = exception switch
            {
                InternalServerException => (
                        exception.Message,
                        exception.GetType().Name,
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError)
                
               ,BadRequestException => (
                             exception.Message,
                             exception.GetType().Name,
                             context.Response.StatusCode = StatusCodes.Status400BadRequest)
               ,ValidationException => (
                             exception.Message,
                             exception.GetType().Name,
                             context.Response.StatusCode = StatusCodes.Status400BadRequest)
               ,NotFoundException => (
                             exception.Message,
                             exception.GetType().Name,
                             context.Response.StatusCode = StatusCodes.Status400BadRequest)
               ,_ => (
                exception.Message,
                        exception.GetType().Name,
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError)

            };

            var problemDetails = new ProblemDetails
            {

                Title = details.Titile,
                Detail =details.Details,
                Status =details.StatusCode,
                Instance = context.Request.Path
            };

            problemDetails.Extensions.Add("traceId",context.TraceIdentifier);

            if(exception is ValidationException validationException)
                problemDetails.Extensions.Add("validationErrors",validationException.Errors);


            await context.Response.WriteAsJsonAsync(problemDetails,cancellationToken);

            return true;
        }
    }
}

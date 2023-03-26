using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Euvic.Api
{
    public static class ExceptionHandlerExtensions
    {
        public static WebApplication ConfigureGlobalExceptionHandling(this WebApplication app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>().Error;
                    var problemDetails = new ValidationProblemDetails();

                    if (exception is ValidationException validationException)
                    {
                        problemDetails.Title = "One or more validation errors occurred.";
                        problemDetails.Status = (int)HttpStatusCode.BadRequest;
                        problemDetails.Detail = validationException.Message;
                        foreach (var key in validationException.Data.Keys)
                        {
                            var value = validationException.Data[key];
                            if (value is string errorMessage)
                            {
                                problemDetails.Errors.Add(key.ToString(), new[] { errorMessage });
                            }
                        }
                    }
                    else
                    {
                        problemDetails.Title = "An unexpected error occurred.";
                        problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                        problemDetails.Detail = exception.Message;
                    }

                    context.Response.ContentType = "application/problem+json";
                    context.Response.StatusCode = problemDetails.Status!.Value;

                    await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
                });
            });

            return app;
        }
    }
}

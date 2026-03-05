//using CoopApplication.api.Exceptions;
//using CoopApplication.Services.Exceptions;
using CoopApplication.api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CoopApplication.api.Middleware
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    context.Response.ContentType = "application/json";

                    context.Response.StatusCode = ex switch
                    {
                        System.ComponentModel.DataAnnotations.ValidationException => StatusCodes.Status400BadRequest,
                        AlreadyExistException => StatusCodes.Status409Conflict,
                        //DuplicateRequisitionException => StatusCodes.Status409Conflict,
                        NotFoundException => StatusCodes.Status404NotFound,
                        //UserValidationException => StatusCodes.Status400BadRequest,
                        SaveOperationException => StatusCodes.Status500InternalServerError,
                        //UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    await context.Response.WriteAsJsonAsync(new
                    {
                        success = false,
                        message = ex?.Message,
                        statusCode = context.Response.StatusCode
                    });
                });
            });
        }
    }
}

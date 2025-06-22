using System.Net;
using System.Text.Json;
using FluentValidation;

namespace TechNest.WebApi.Middlewares;

public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    IWebHostEnvironment env,
    ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var errorResponse = new ErrorResponse
        {
            Timestamp = DateTime.UtcNow,
            Path = context.Request.Path
        };

        switch (exception)
        {
            case ValidationException validationEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.StatusCode = context.Response.StatusCode;
                errorResponse.Message = "Validation failed";
                errorResponse.Errors = validationEx.Errors
                    .Select(e => new ValidationError
                    {
                        Field = e.PropertyName,
                        Message = e.ErrorMessage
                    }).ToList();
                break;

            case UnauthorizedAccessException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.StatusCode = context.Response.StatusCode;
                errorResponse.Message = "Unauthorized access";
                break;

            case KeyNotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.StatusCode = context.Response.StatusCode;
                errorResponse.Message = exception.Message;
                break;

            case NotImplementedException:
                context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                errorResponse.StatusCode = context.Response.StatusCode;
                errorResponse.Message = "This feature is not implemented.";
                break;

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.StatusCode = context.Response.StatusCode;
                errorResponse.Message = "An unexpected error occurred.";

                logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
                break;
        }

        if (env.IsDevelopment())
        {
            errorResponse.Details = exception.ToString(); 
        }

        var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        });

        await context.Response.WriteAsync(json);
    }

    private class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public string Path { get; set; } = null!;
        public List<ValidationError>? Errors { get; set; }
        public string? Details { get; set; }
    }

    private class ValidationError
    {
        public string Field { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
using System.Net;
using System.Text.Json;
using KuyumcuWebApi.dto;

namespace KuyumcuWebApi.middeware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"Unhandled exception: {ex.Message}");

            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Internal Server Error", "An unexpected error occurred.");
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string error, string message)
    {
        var errorResponse = new ErrorResponse
        {
            Error = error,
            Message = message,
            Details = string.Empty
        };

        var jsonResponse = JsonSerializer.Serialize(errorResponse);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(jsonResponse);
    }
}
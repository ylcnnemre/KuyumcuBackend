using System.Net;
using System.Text.Json;
using KuyumcuWebApi.dto;
using KuyumcuWebApi.exception;

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
        catch (UnauthorizedException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";
            var errorResponse = new ErrorResponse
            {
                Error = "Unauthorize",
                Message = ex.Message,
                Details = string.Empty
            };
            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (ConflictException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Error = "Conflict",
                Message = ex.Message,
                Details = string.Empty
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Error = "NotFound",
                Message = ex.Message,
                Details = string.Empty
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (BadRequestException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Error = "Bad Request",
                Message = ex.Message,
                Details = string.Empty
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework here)
            Console.WriteLine($"Unhandled exception: {ex.Message}");

            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Internal Server Error", ex.Message);
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
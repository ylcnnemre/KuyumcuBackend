using System.Net;
using System.Text.Json;
using KuyumcuWebApi.dto;

namespace KuyumcuWebApi.middeware;

public class CustomAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
        {
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Error = "Unauthorized",
                Message = "You are not authorized to access this resource.",
                Details = string.Empty
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }

        if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
        {
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Error = "Forbidden",
                Message = "You do not have permission to access this resource.",
                Details = string.Empty
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}

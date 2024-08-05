using KuyumcuWebApi.dto;
using KuyumcuWebApi.exception;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace KuyumcuWebApi.middleware
{
    public class CustomAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
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
                /* else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    context.Response.ContentType = "application/json";

                    var errorResponse = new ErrorResponse
                    {
                        Error = "Forbidden",
                        Message = "You are not authorized to access this resource.",
                        Details = string.Empty
                    };

                    var jsonResponse = JsonSerializer.Serialize(errorResponse);
                    await context.Response.WriteAsync(jsonResponse);
                } */
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
            catch (Exception ex)
            {
                // Diğer istisnalar için genel bir işleyici
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new ErrorResponse
                {
                    Error = "Internal Server Error",
                    Message = ex.Message,
                    Details = string.Empty
                };

                var jsonResponse = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}

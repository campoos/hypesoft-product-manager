using System.Net;
using System.Text.Json;
using Hypesoft.Domain.Exceptions;
using Hypesoft.Application.Exceptions;

namespace Hypesoft.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainValidationException ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.BadRequest);
            }
            catch (NotFoundException ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception)
            {
                await HandleException(context, "Erro interno no servidor.", HttpStatusCode.InternalServerError);
            }
        }

        private static async Task HandleException(HttpContext context, string message, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                message = message,
                status_code = (int)statusCode,
                timestamp = DateTime.UtcNow,
            };

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}
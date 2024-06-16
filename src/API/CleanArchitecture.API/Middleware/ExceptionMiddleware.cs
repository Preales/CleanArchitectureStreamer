using CleanArchitecture.API.Errors;
using CleanArchitecture.Application.Exceptions;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace CleanArchitecture.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = MediaTypeNames.Application.Json;
            var statusCode = HttpStatusCode.InternalServerError;
            string result = string.Empty;

            switch (ex)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case CustomValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    var validationJson = JsonSerializer.Serialize(validationException.Errors);
                    result = JsonSerializer.Serialize(new CodeErrorException((int)statusCode, ex.Message, validationJson));
                    break;
                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                default:
                    break;
            }

            if (!string.IsNullOrWhiteSpace(result))
                result = JsonSerializer.Serialize(new CodeErrorException((int)statusCode, ex.Message, ex.StackTrace));


            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(result);

        }
    }
}

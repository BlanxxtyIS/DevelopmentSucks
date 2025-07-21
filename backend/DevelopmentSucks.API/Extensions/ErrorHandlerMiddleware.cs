using DevelopmentSucks.Application.Contracts.DTO;

namespace DevelopmentSucks.API.Extensions;

/// <summary>
/// Global Exception Middleware - единое централизированное место,
/// где перехватываются необработанные исключения из всего пайплайна.
/// </summary>
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next,
        ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла необработанная ошибка");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error",
                Detailed = ex.Message // ❗ убрать в проде
            };

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    } 
}

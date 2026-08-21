using System.Net;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DevFlix.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message) = exception switch
        {
            DbUpdateException dbEx => GetDbUpdateExceptionResponse(dbEx),
            InvalidOperationException => (HttpStatusCode.Conflict, exception.Message),
            _ => (HttpStatusCode.InternalServerError, exception.Message)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
        {
            error = message
        }));
    }

    private static (HttpStatusCode, string) GetDbUpdateExceptionResponse(DbUpdateException ex)
    {
        var sqlException = ex.InnerException;

        while (sqlException != null)
        {
            if (sqlException is SqlException sql && (sql.Number == 2601 || sql.Number == 2627))
            {
                return (HttpStatusCode.Conflict, "A resource with the same identifier already exists.");
            }

            sqlException = sqlException.InnerException;
        }

        return (HttpStatusCode.InternalServerError, ex.Message);
    }
}

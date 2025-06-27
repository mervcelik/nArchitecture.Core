using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.SeriLog;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private LoggerServiceBase _loggerService;
    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor, LoggerServiceBase loggerService)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _httpExceptionHandler = new HttpExceptionHandler();
        _httpContextAccessor = httpContextAccessor;
        _loggerService = loggerService;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await LogException(context, ex);
            await HandleExceptionAsync(context.Response, ex);
        }
    }

    private Task LogException(HttpContext context, Exception ex)
    {
        List<LogParameter> logParameters = new()
        {
            new LogParameter{Type=context.GetType().Name, Value=ex.ToString()}
        };

        LogDetailWithException logDetail = new()
        {
            ExceptionMessage = ex.Message,
            MethodName = _next.Method.Name,
            LogParameters = logParameters,
            User = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "?"
        };

        _loggerService.Error(JsonSerializer.Serialize(logDetail));

        return Task.CompletedTask;
    }

    private async Task HandleExceptionAsync(HttpResponse response, Exception ex)
    {
        response.ContentType = "application/problem+json";
        _httpExceptionHandler.Response = response;
        await _httpExceptionHandler.HandleExceptionAsync(ex);
    }
}

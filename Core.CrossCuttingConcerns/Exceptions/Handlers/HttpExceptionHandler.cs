using Core.CrossCuttingConcerns.Dtos;
using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers;

public class HttpExceptionHandler : ExceptionHandler
{
    public HttpResponse _response;
    public HttpResponse Response
    {
        get => _response ?? throw new ArgumentNullException(nameof(_response));
        set => _response = value;
    }

    private Task WriteErrorResponse(int statusCode, string errorMessage)
    {
        var responseDto = ResponseDto<object>.Fail(statusCode, errorMessage);
        Response.StatusCode = statusCode;
        Response.ContentType = "application/json";
        var json = JsonSerializer.Serialize(responseDto);
        return Response.WriteAsync(json);
    }

    private Task WriteErrorResponse(int statusCode, List<string> errors)
    {
        var responseDto = ResponseDto<object>.Fail(statusCode, errors);
        Response.StatusCode = statusCode;
        Response.ContentType = "application/json";
        var json = JsonSerializer.Serialize(responseDto);
        return Response.WriteAsync(json);
    }

    protected override Task HandleException(BusinessException businessException)
    {
        return WriteErrorResponse(StatusCodes.Status400BadRequest, businessException.Message);
    }

    protected override Task HandleException(Exception exception)
    {
        return WriteErrorResponse(StatusCodes.Status500InternalServerError, "Beklenmeyen bir hata oluştu."+exception.Message);
    }

    protected override Task HandleException(ValidationException validationException)
    {
        var errors = validationException.Errors.Select(e => e.Errors).ToList();
        var x= errors.SelectMany(e => e).ToList();
        return WriteErrorResponse(StatusCodes.Status400BadRequest, x);
    }

    public override Task HandleException(NotFoundException notFoundException)
    {
        return WriteErrorResponse(StatusCodes.Status404NotFound, notFoundException.Message);
    }

    protected override Task HandleException(AuthorizationException authorizationException)
    {
        return WriteErrorResponse(StatusCodes.Status403Forbidden, authorizationException.Message);
    }
}

﻿using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    protected override Task HandleException(BusinessException businessException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        string details = new BusinessProblemDetails(businessException.Message).AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(Exception businessException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        string details = new InternalServerErrorProblemDetails().AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(ValidationException validationException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        string details = new ValidationProblemDetails(validationException.Errors).AsJson();
        return Response.WriteAsync(details);
    }
    public override Task HandleException(NotFoundException notFoundException)
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        string details = new NotFoundProblemDetails(notFoundException.Message).AsJson();
        return Response.WriteAsync(details);
    }

    protected override Task HandleException(AuthorizationException authorizationException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        string details = new AuthorizationProblemDetails(authorizationException.Message).AsJson();
        return Response.WriteAsync(details);
    }
}

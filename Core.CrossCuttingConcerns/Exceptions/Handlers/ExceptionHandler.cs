﻿using Core.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers;

public abstract class ExceptionHandler
{
    public Task HandleExceptionAsync(Exception exception) =>exception switch
        {
            BusinessException businessException => HandleException(businessException),
            ValidationException validationException => HandleException(validationException),
            AuthorizationException authorizationException => HandleException(authorizationException),
            _ => HandleException(exception)
        };

    protected abstract Task HandleException(BusinessException businessException);
    protected abstract Task HandleException(ValidationException validationException);
    protected abstract Task HandleException(AuthorizationException authorizationException);
    public abstract Task HandleException(NotFoundException notFoundException);
    protected abstract Task HandleException(Exception businessException);
}

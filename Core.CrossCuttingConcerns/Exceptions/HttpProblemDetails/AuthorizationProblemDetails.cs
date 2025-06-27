using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

internal class AuthorizationProblemDetails : ProblemDetails
{
    public AuthorizationProblemDetails(string detail)
    {
        Title = "Authorization Violation";
        Detail = detail;
        Status = StatusCodes.Status401Unauthorized;
        Type = "https://Example.com/probs/business";
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class InternalServerErrorProblemDetails : ProblemDetails
{
    public InternalServerErrorProblemDetails()
    {
        Title= "InternalServerError";
        Detail = "InternalServerError";
        Status = StatusCodes.Status500InternalServerError;
        Type = "https://Example.com/probs/business";
    }
}

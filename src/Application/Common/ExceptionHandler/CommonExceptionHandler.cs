using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Blazor.Application.Constants;

namespace CleanArchitecture.Blazor.Application.Common.ExceptionHandler;
public class GenericExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException> 
    where TRequest:notnull 
    where TException : Exception
{
    private readonly ILogger<TRequest> _logger;

    public GenericExceptionHandler(ILogger<TRequest> logger)
    {
        _logger = logger;
    }
    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        var requestName = nameof(request);
        _logger.LogError(exception, "Application Request: Exception for Request {Name} {@Request}", requestName, request);
        state.SetHandled(default);
        return Task.CompletedTask;
    }
}
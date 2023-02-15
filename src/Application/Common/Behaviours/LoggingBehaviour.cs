// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.



namespace CleanArchitecture.Blazor.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;


    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;

    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = nameof(TRequest);
        var userName = _currentUserService.UserName;
        _logger.LogTrace("Application Request: {Name} by {@UserName} {@Request}",
            requestName,userName, request);
        return Task.CompletedTask;
    }
}

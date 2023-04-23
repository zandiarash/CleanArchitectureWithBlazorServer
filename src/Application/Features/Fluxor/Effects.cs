﻿using System.Net.Http.Json;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Identity;
using CleanArchitecture.Blazor.Application.Common.Interfaces.Identity.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Fluxor;
public class Effects
{
   
    private readonly IIdentityService _identityService;

    public Effects(IIdentityService identityService)
    {
      
        _identityService = identityService;
    }
    [EffectMethod]
    public async Task HandleFetchDataAction(FetchUserDtoAction action, IDispatcher dispatcher)
    {
        var result = await _identityService.GetUser(action.UserId);
        if(result is not null)
            dispatcher.Dispatch(new FetchUserDtoResultAction(result));
    }
}

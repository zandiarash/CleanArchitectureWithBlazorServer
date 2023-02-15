// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Common.ExceptionHandler;

public class ResourceNotFoundException : ServerException
{


    public ResourceNotFoundException(string message)
        : base(message,null,System.Net.HttpStatusCode.NotFound)
    {
    }



    public ResourceNotFoundException(string name, object key)
        : base($"Resource \"{name}\" ({key}) was not found.", null, System.Net.HttpStatusCode.NotFound)
    {
    }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Trucks.Commands.Delete;

public class DeleteTruckCommandValidator : AbstractValidator<DeleteTruckCommand>
{
    public DeleteTruckCommandValidator()
    {

        RuleFor(v => v.Id).NotNull().NotEmpty().ForEach(x => x.GreaterThan(0));

    }
}



// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Commands.AddEdit;

public class AddEditShippingOrderCommandValidator : AbstractValidator<AddEditShippingOrderCommand>
{
    public AddEditShippingOrderCommandValidator()
    {

        RuleFor(v => v.OrderNo)
            .Length(12)
            .NotEmpty();
        RuleFor(v => v.StartingTime)
                 .NotEmpty();
        RuleFor(v => v.FinishTime)
                 .GreaterThan(e => e.StartingTime);
        RuleFor(v => v.TruckId)
            .GreaterThan(0);
        RuleFor(v => v.DriverName).NotEmpty();
        RuleFor(v => v.PhoneNumber).NotEmpty();

    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<AddEditShippingOrderCommand>.CreateWithOptions((AddEditShippingOrderCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}


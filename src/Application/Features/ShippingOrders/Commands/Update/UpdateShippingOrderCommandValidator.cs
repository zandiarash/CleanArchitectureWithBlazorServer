// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Commands.Update;

    public class UpdateShippingOrderCommandValidator : AbstractValidator<UpdateShippingOrderCommand>
    {
        public UpdateShippingOrderCommandValidator()
        {
        RuleFor(v => v.OrderNo)
         .Length(8)
         .NotEmpty();
        RuleFor(v => v.StartingTime)
                 .NotNull();
        RuleFor(v => v.FinishTime)
                 .GreaterThan(e => e.StartingTime);
        RuleFor(v => v.TruckId)
            .GreaterThan(0);
        RuleFor(v => v.Driver).NotEmpty();
        RuleFor(v => v.PhoneNumber).NotEmpty();
    }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
     {
        var result = await ValidateAsync(ValidationContext<UpdateShippingOrderCommand>.CreateWithOptions((UpdateShippingOrderCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
     };
    }


// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Drivers.Commands.Update;

    public class UpdateDriverCommandValidator : AbstractValidator<UpdateDriverCommand>
    {
        public UpdateDriverCommandValidator()
        {
        RuleFor(v => v.Name)
          .MaximumLength(256)
          .NotEmpty();
        RuleFor(v => v.PhoneNumber)
                .MaximumLength(256)
                .NotEmpty();
        RuleFor(v => v.DrivingType)
                .MaximumLength(256)
                .NotEmpty();
        RuleFor(v => v.BrithDay)
               .LessThan(DateTime.Now);
    }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
     {
        var result = await ValidateAsync(ValidationContext<UpdateDriverCommand>.CreateWithOptions((UpdateDriverCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
     };
    }


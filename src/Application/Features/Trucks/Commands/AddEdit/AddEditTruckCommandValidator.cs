// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.RegularExpressions;

namespace CleanArchitecture.Blazor.Application.Features.Trucks.Commands.AddEdit;

public class AddEditTruckCommandValidator : AbstractValidator<AddEditTruckCommand>
{
    public AddEditTruckCommandValidator()
    {

        RuleFor(v => v.PlateNumber)
             .NotEmpty()
             .NotNull()
             .MinimumLength(7)
             .MaximumLength(8)
             .Must(x =>
             {
                 if (string.IsNullOrEmpty(x)) return false;
                 bool result = false;
                 string carnumRegex = @"([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}(([0-9]{5}[DF])|([DF]([A-HJ-NP-Z0-9])[0-9]{4})))|([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[A-HJ-NP-Z0-9]{4}[A-HJ-NP-Z0-9学警港澳]{1})";
                 result = Regex.IsMatch(x, carnumRegex);
                 return result;
             });

    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<AddEditTruckCommand>.CreateWithOptions((AddEditTruckCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}


// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.RegularExpressions;

namespace CleanArchitecture.Blazor.Application.Features.Trucks.Commands.Update;

    public class UpdateTruckCommandValidator : AbstractValidator<UpdateTruckCommand>
    {
        public UpdateTruckCommandValidator()
        {
        RuleFor(v => v.PlateNumber)
             .MinimumLength(7)
             .MaximumLength(8)
             .NotEmpty()
             .Must(x => {
                 bool result = false;
                 string carnumRegex = @"([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]
                                    {1}[A-Z]{1}(([0-9]{5}[DF])|([DF]([A-HJ-NP-Z0-9])[0-9]{4})))|([京津沪渝冀豫云
                                    辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[A-HJ-NP-Z0-9]
                                    {4}[A-HJ-NP-Z0-9学警港澳]{1})";
                 result = Regex.IsMatch(x, carnumRegex);
                 return result;
             });
    }
    }


// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Drivers.Commands.Import;

    public class ImportDriversCommandValidator : AbstractValidator<ImportDriversCommand>
    {
        public ImportDriversCommandValidator()
        {
         
            RuleFor(v => v.Data)
                 .NotEmpty();
        
        }
    }


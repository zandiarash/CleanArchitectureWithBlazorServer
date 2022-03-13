// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.ShippingOrders.Commands.Delete;

    public class DeleteShippingOrderCommandValidator : AbstractValidator<DeleteShippingOrderCommand>
    {
        public DeleteShippingOrderCommandValidator()
        {
          
           RuleFor(v => v.Id).NotNull().GreaterThan(0);
     
        }
    }
    public class DeleteCheckedShippingOrdersCommandValidator : AbstractValidator<DeleteCheckedShippingOrdersCommand>
    {
        public DeleteCheckedShippingOrdersCommandValidator()
        {
          
            RuleFor(v => v.Id).NotNull().NotEmpty();
         
        }
    }


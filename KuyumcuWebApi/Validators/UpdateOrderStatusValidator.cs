using FluentValidation;
using KuyumcuWebApi.dto;

namespace KuyumcuWebApi.Validators;


public class UpdateOrderStatusValidator : AbstractValidator<OrderUpdateStatusDto>
{
    public UpdateOrderStatusValidator()
    {
        int[] allowedIds = {1,2,3};
        RuleFor(item => item.OrderStatusId).Must(el =>  allowedIds.Contains(el)).WithMessage("Order StatusId 1,2 veya 3 olmalı");
    }
}
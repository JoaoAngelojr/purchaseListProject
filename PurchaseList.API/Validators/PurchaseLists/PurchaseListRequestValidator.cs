using FluentValidation;
using PurchaseList.API.Requests;
using System.Linq;

namespace PurchaseList.API.Validators.PurchaseLists
{
    public class PurchaseListRequestValidator : AbstractValidator<CalculateBillsRequest>
    {
        public PurchaseListRequestValidator()
        {
            RuleFor(request => request.Emails)
                .Must(emails => emails.Count > 0)
                .WithMessage("You need to add at least one email.");

            RuleFor(request => request.Emails)
                .Must(emails => !emails.Any(x => string.IsNullOrEmpty(x)))
                .WithMessage("None of emails cannot be null or empty");

            RuleFor(request => request.Items)
                .Must(items => items.Count > 0)
                .WithMessage("You need to add at least one item.");

            RuleFor(request => request.Items)
                .Must(items => !items.Any(x => x.Quantity < 0))
                .WithMessage("The item quantity cannot be a negative number.");

            RuleFor(request => request.Items)
                .Must(items => !items.Any(x => x.Price < 0))
                .WithMessage("The item price cannot be a negative number.");
        }
    }
}
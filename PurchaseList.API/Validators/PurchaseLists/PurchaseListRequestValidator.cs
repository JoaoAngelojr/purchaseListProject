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
                .Must(emails => emails != null && emails.Count > 0)
                .WithMessage("You need to add at least one email.");

            RuleFor(request => request.Emails)
                .Must(emails => !emails.Any(x => string.IsNullOrEmpty(x)))
                .When(request => request.Emails != null)
                .WithMessage("None of emails can be null or empty.");

            RuleFor(request => request.Emails)
                .Must(emails => !emails.Any(x => string.IsNullOrEmpty(x.Trim())))
                .When(request => request.Emails != null && !request.Emails.Any(x => string.IsNullOrEmpty(x)))
                .WithMessage("None of emails can be null or empty.");

            RuleFor(request => request.Emails)
                .Must(emails => emails.GroupBy(x => x).Where(y => y.Count() > 1).Count() == 0)
                .When(request => request.Emails != null && !request.Emails.Any(x => string.IsNullOrEmpty(x.Trim())))
                .WithMessage("The list of emails cannot have equal emails.");

            RuleFor(request => request.Items)
                .Must(items => items != null && items.Count > 0)
                .WithMessage("You need to add at least one item.");

            RuleFor(request => request.Items)
                .Must(items => !items.Any(x => x.Quantity < 0))
                .When(request => request.Items != null)
                .WithMessage("The item quantity cannot be a negative number.");

            RuleFor(request => request.Items)
                .Must(items => !items.Any(x => x.Price < 0))
                .When(request => request.Items != null)
                .WithMessage("The item price cannot be a negative number.");

            RuleFor(request => request.Items)
                .Must(items => !items.Any(x => string.IsNullOrEmpty(x.Name)))
                .When(request => request.Items != null)
                .WithMessage("None of items names can be null or empty.");

            RuleFor(request => request.Items)
                .Must(items => !items.Any(x => string.IsNullOrEmpty(x.Name.Trim())))
                .When(request => request.Items != null && !request.Items.Any(x => string.IsNullOrEmpty(x.Name)))
                .WithMessage("None of items names can be null or empty.");
        }
    }
}
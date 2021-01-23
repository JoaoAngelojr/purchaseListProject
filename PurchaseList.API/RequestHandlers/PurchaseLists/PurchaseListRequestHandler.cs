using MediatR;
using PurchaseList.API.Requests;
using PurchaseList.API.ViewModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PurchaseList.API.RequestHandlers.PurchaseLists
{
    public sealed class PurchaseListRequestHandler : IRequestHandler<CalculateBillsRequest, ResultViewModel>
    {
        public PurchaseListRequestHandler() { }

        public async Task<ResultViewModel> Handle(CalculateBillsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ResultViewModel result = new ResultViewModel();

                int total = request.Items.Select(x => x.Quantity * x.Price).Sum();
                int emailsQuantity = request.Emails.Count;
                int restOfDivision = total % emailsQuantity;
                int billValue = total / emailsQuantity;
                int toReceiveExcedent = emailsQuantity - restOfDivision;

                foreach (string email in request.Emails)
                {
                    if (request.Emails.IndexOf(email) == toReceiveExcedent)
                    {
                        result.BillsPayable.Add(email, (billValue + 1));
                        toReceiveExcedent += 1;
                    }
                    else
                    {
                        result.BillsPayable.Add(email, billValue);
                    }
                }

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
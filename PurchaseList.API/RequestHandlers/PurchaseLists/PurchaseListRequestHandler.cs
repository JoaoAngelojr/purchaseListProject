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
                int total = request.Items.Select(x => x.Quantity * x.Price).Sum();
                int emailsQuantity = request.Emails.Count;
                int restOfDivision = total % emailsQuantity;
                int billValue = total / emailsQuantity;
                ResultViewModel result = new ResultViewModel();

                foreach ((string email, int indexOfLastEmail) in from string email in request.Emails
                                                                 let indexOfLastEmail = emailsQuantity - 1
                                                                 select (email, indexOfLastEmail))
                    CreateBillsDictionary(request, restOfDivision, billValue, result, email, indexOfLastEmail);

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        private static void CreateBillsDictionary(
            CalculateBillsRequest request,
            int restOfDivision,
            int billValue,
            ResultViewModel result,
            string email,
            int indexOfLastEmail)
        {
            if (restOfDivision == 0 && request.Emails.IndexOf(email) == indexOfLastEmail)
            {
                result.BillsPayable.Add(email, (billValue + restOfDivision));
            }
            else
            {
                result.BillsPayable.Add(email, (billValue + restOfDivision));
            }
        }
    }
}
using PurchaseList.API.RequestHandlers.PurchaseLists;
using PurchaseList.API.Requests;
using PurchaseList.API.ViewModels;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace PurchaseList.Tests.RequestHandlerTests
{
    public class PurchaseListRequestHandlerTests
    {
        private readonly PurchaseListRequestHandler _handler;

        public PurchaseListRequestHandlerTests()
        {
            _handler = new PurchaseListRequestHandler();
        }

        [Fact]
        public void WhenPassValidProcessDataRequestThenShouldBeSuccess()
        {
            List<ItemViewModel> items = new List<ItemViewModel>();
            ItemViewModel item1 = new ItemViewModel()
            {
                Quantity = 1,
                Price = 5
            };
            items.Add(item1);

            ItemViewModel item2 = new ItemViewModel()
            {
                Quantity = 2,
                Price = 3
            };
            items.Add(item2);

            List<string> emails = new List<string>();
            emails.Add("email1@email.com");
            emails.Add("email2@email.com");

            CalculateBillsRequest request = new CalculateBillsRequest()
            {
                Items = items,
                Emails = emails
            };

            ResultViewModel result = _handler.Handle(request, default(CancellationToken)).GetAwaiter().GetResult();
            Assert.True(result != null);
            Assert.True(result.BillsPayable != null);
            Assert.True(result.BillsPayable.Count > 0);
        }
    }
}
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NSubstitute;
using PurchaseList.API.Controllers;
using PurchaseList.API.Requests;
using PurchaseList.API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PurchaseList.Tests.ControllersTests
{
    public class PurchaseListControllerTests
    {
        private readonly IMediator _mediator;
        private readonly PurchaseListController _controller;

        public PurchaseListControllerTests()
        {
            _mediator = Substitute.For<IMediator>();
            _controller = new PurchaseListController(_mediator);
        }

        [Fact]
        public async Task WhenSendValidRequestForCalculateBillsThenShouldBeSuccess()
        {
            List<ItemViewModel> items = new List<ItemViewModel>();
            ItemViewModel item1 = new ItemViewModel()
            {
                Name = "coffee",
                Quantity = 1,
                Price = 5
            };
            items.Add(item1);

            ItemViewModel item2 = new ItemViewModel()
            {
                Name = "sugar",
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

            ResultViewModel result = new ResultViewModel();
            Dictionary<string, int> billsPayable = new Dictionary<string, int>();
            billsPayable.Add("email1@email.com", 5);
            billsPayable.Add("email2@email.com", 6);
            result.BillsPayable = billsPayable;

            _mediator.Send(request).Returns(result);

            IActionResult response = await _controller.CalculateBills(request);
            IStatusCodeActionResult status = (IStatusCodeActionResult)response;

            Assert.True(status.StatusCode == StatusCodes.Status200OK);
        }

        [Fact]
        public async Task WhenSendInvalidRequestForCalculateBillsThenShouldFail()
        {
            CalculateBillsRequest invalidRequest = new CalculateBillsRequest();

            IActionResult response = await _controller.CalculateBills(invalidRequest);
            IStatusCodeActionResult status = (IStatusCodeActionResult)response;

            Assert.True(status.StatusCode == StatusCodes.Status400BadRequest);
        }
    }
}
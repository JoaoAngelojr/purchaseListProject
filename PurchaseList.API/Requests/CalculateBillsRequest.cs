using MediatR;
using PurchaseList.API.ViewModels;
using System.Collections.Generic;

namespace PurchaseList.API.Requests
{
    public class CalculateBillsRequest : IRequest<ResultViewModel>
    {
        public List<ItemViewModel> Items { get; set; }
        public List<string> Emails { get; set; }
    }
}
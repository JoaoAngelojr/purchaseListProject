using System.Collections.Generic;

namespace PurchaseList.API.ViewModels
{
    public class ResultViewModel
    {
        public ResultViewModel ()
        {
            BillsPayable = new Dictionary<string, int>();
        }

        public Dictionary<string, int> BillsPayable { get; set; }
    }
}
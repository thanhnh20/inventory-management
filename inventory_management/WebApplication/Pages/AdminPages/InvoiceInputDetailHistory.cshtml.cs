using Library.Model;
using Library.Repository;
using Library.Repository.RepositoryImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Pages.AdminPages
{
    public class InvoiceInputDetailHistoryModel : PageModel
    {

        public string Error { get; set; }


        IConsignmentDetailRepository consignmentDetailRepository;

        IInvoiceInputDetailRepository invoiceInputDetailRepository;

        public IEnumerable<IGrouping<int, ConsignmentDetail>> ConsignmentDetails { get; set; }

        public InvoiceInputDetailHistoryModel(IConsignmentDetailRepository consignmentDetailRepository)
        {
            this.consignmentDetailRepository = consignmentDetailRepository;
            this.invoiceInputDetailRepository = new InvoiceInputDetailRepositoryIml();
        }

        public IActionResult OnGet(int inputBillID)
        {
            var accountJson = HttpContext.Session.GetString("ADMIN");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "accountJson is null";
                return Page();
            }
            else
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if (AccountSession == null)
                {
                    Error = "accountSession is null";
                    return Page();
                }
                else
                {
                    var invoiceinputDetail = invoiceInputDetailRepository.getConsignmentDetailIDByInputBill(inputBillID);
                    var consignmentID = consignmentDetailRepository.GetConsignmentIDDetailsByID(invoiceinputDetail.ConsignmentDetailId);
                    ConsignmentDetails = consignmentDetailRepository.GetConsignmentDetails(consignmentID);
                }
            }
            return Page();
        }
    }
}

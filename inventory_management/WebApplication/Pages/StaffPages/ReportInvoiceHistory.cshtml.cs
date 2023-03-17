using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using Library.Repository;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Library.Repository.RepositoryImpl;

namespace WebApplication.Pages.StaffPages
{
    public class ReportInvoiceHistoryModel : PageModel
    {

        public string Error { get; set; }
        IInvoiceInputRepository invoiceInputRepository;

        IProductRepository productRepository;

        IConsignmentDetailRepository consignmentDetailRepository;

        public ReportInvoiceHistoryModel(IProductRepository productRepository, IConsignmentDetailRepository consignmentDetailRepository)
        {
            this.consignmentDetailRepository = consignmentDetailRepository;
            this.productRepository = productRepository;
            this.invoiceInputRepository = new InvoiceInputRepository();
        }

        public IList<InvoiceInput> InvoiceInput { get;set; }

        public IList<Product> ProductHasConsignment { get; set; }

        public IEnumerable<IGrouping<int, ConsignmentDetail>> ConsignmentDetails { get; set; }

        public IActionResult OnGet()
        {
            var accountJson = HttpContext.Session.GetString("STAFF");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "accountJson is null";
                return Redirect("~/HomePages/Home");
            }
            else
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if (accountJson == null)
                {
                    Error = "accountSession is null";
                    return Redirect("~/HomePages/Home");
                }
                else
                {
                    /*var listResultInvoice = invoiceInputRepository.ListAll();
                    var listHasInConsignment = productRepository.GetAllProducInConsignment();                
                    //InvoiceInput = listResultInvoice;
                    ProductHasConsignment = listHasInConsignment;*/
                    ConsignmentDetails = consignmentDetailRepository.GetConsignmentDetails();
                }
            }
            return Page();
        }
    }
}

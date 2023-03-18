using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using Library.Repository;
using Library.Repository.RepositoryImpl;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApplication.Pages.AdminPages
{
    public class InvoiceInputReportModel : PageModel
    {
        public string Error { get; set; }

        IInvoiceInputRepository invoiceInputRepository;

        public InvoiceInputReportModel()
        {
            this.invoiceInputRepository = new InvoiceInputRepository();
        }

        public IList<InvoiceInput> InvoiceInput { get;set; }

        public IActionResult OnGet()
        {
            var accountJson = HttpContext.Session.GetString("ADMIN");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "accountJson is null";
                return Redirect("~/HomePages/Home");
            }
            else
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if (AccountSession == null)
                {
                    Error = "accountSession is null";
                    return Redirect("~/HomePages/Home");
                }
                else
                {
                    InvoiceInput = invoiceInputRepository.ListAll();
                }
            }
            return Page();
        }
    }
}

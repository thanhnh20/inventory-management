using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using WebApplication.Models;
using Library.Repository;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebApplication.Pages.AdminPages
{
    public class RevenueModel : PageModel
    {
        private readonly IInvoiceOutputRepository _invoiceOutputRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IUserRepository _userRepo;
        private readonly IInvoiceOutputDetailRepository _invoiceOutputDetailRepo;

        public RevenueModel(IInvoiceOutputRepository invoiceOutputRepo, 
            ICustomerRepository customerRepo, IUserRepository userRepo, IInvoiceOutputDetailRepository invoiceOutputDetailRepo)
        {
            _invoiceOutputRepo = invoiceOutputRepo;
            _customerRepo = customerRepo;
            _userRepo = userRepo;
            _invoiceOutputDetailRepo = invoiceOutputDetailRepo;
        }

        public IList<RevenueViewModel> Revenues { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var accountJson = HttpContext.Session.GetString("ADMIN");
            if (string.IsNullOrEmpty(accountJson))
            {
                return RedirectToPage("../AdminPages/MainPage");
            }
            var account = JsonConvert.DeserializeObject<User>(accountJson);
            if (account == null)
            {
                return RedirectToPage("../AdminPages/MainPage");
            }
            var invoiceOutputs = await _invoiceOutputRepo.GetMany();
            var revenues = new List<RevenueViewModel>();
            if (invoiceOutputs == null || invoiceOutputs.Count() == 0)
            {
                Revenues = revenues;
                return Page();
            }
            foreach (var invoiceOutput in invoiceOutputs)
            {
                var customer = await _customerRepo.GetOne(cus => cus.CustomerId == invoiceOutput.CustomerId);
                if (customer == null)
                {
                    continue;
                }
                var user = await _userRepo.GetOne(u => u.UserId == invoiceOutput.UserId);
                if (user == null)
                {
                    continue;
                }
                var invoiceOutputDetails = await _invoiceOutputDetailRepo.GetMany(invoiceD => invoiceD.OutputBillId == invoiceOutput.OutputBillId);
                double totalPrice = 0;
                if (invoiceOutputDetails != null && invoiceOutputDetails.Count() > 0)
                {
                    foreach (var invoiceOutputDetail in invoiceOutputDetails)
                    {
                        if (invoiceOutputDetail.TotalPrice != null)
                        {
                            totalPrice += invoiceOutputDetail.TotalPrice.Value;
                        }
                    }
                    revenues.Add(new RevenueViewModel()
                    {
                        CustomerName = customer.CustomerName,
                        OutputDate = invoiceOutput.OutputDate,
                        Amount = invoiceOutput.Amount,
                        OutputBillId = invoiceOutput.OutputBillId,
                        UserName = user.FullName,
                        TotalPrice = totalPrice,
                    });
                }
            }
            Revenues = revenues;
            return Page();
        }
    }
}

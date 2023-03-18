using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Model;
using WebApplication.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Library.Repository;

namespace WebApplication.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateModel(IMapper mapper, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            var accountJson = HttpContext.Session.GetString("STAFF");
            if (string.IsNullOrEmpty(accountJson))
            {
                return RedirectToPage("../StaffPages/MainPage");
            }
            var account = JsonConvert.DeserializeObject<User>(accountJson);
            if (account == null)
            {
                return RedirectToPage("../StaffPages/MainPage");
            }
            return Page();
        }

        [BindProperty]
        public CustomerViewModel Customer { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var accountJson = HttpContext.Session.GetString("STAFF");
            if (string.IsNullOrEmpty(accountJson))
            {
                return RedirectToPage("../StaffPages/MainPage");
            }
            var account = JsonConvert.DeserializeObject<User>(accountJson);
            if (account == null)
            {
                return RedirectToPage("../StaffPages/MainPage");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customer = await _customerRepository.GetOne(cus => cus.CustomerName.ToLower().Equals(Customer.CustomerName.ToLower()));
            if(customer != null)
            {
                ViewData["Error"] = "Already exist customer name!";
                return Page();
            }
            customer = await _customerRepository.GetOne(cus => cus.CustomerAddress.ToLower().Equals(Customer.CustomerAddress.ToLower()));
            if (customer != null)
            {
                ViewData["Error"] = "Already exist customer address!";
                return Page();
            }
            customer = await _customerRepository.GetOne(cus => cus.CustomerPhone.ToLower().Equals(Customer.CustomerPhone.ToString().ToLower()));
            if (customer != null)
            {
                ViewData["Error"] = "Already exist Phone number!";
                return Page();
            }
            customer = _mapper.Map<Customer>(Customer);
            if(customer != null)
            {
                bool result = await _customerRepository.Add(customer);
                if(result)
                {
                    return RedirectToPage("./Index");
                }
            }
            return Page();
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using WebApplication.Models;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Library.Repository;

namespace WebApplication.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public EditModel(IMapper mapper, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [BindProperty]
        public CustomerViewModel Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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
            if (id == null)
            {
                return NotFound();
            }
            Customer = _mapper.Map<CustomerViewModel>(await _customerRepository.GetOne(s => s.CustomerId == id));

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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
            /*
            var customer = await _customerRepository.GetOne(cus => cus.CustomerName.ToLower().Equals(Customer.CustomerName.ToLower()));
            if (customer != null)
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
            }*/
            var customer = _mapper.Map<Customer>(Customer);
            if (customer == null)
            {
                return Page();
            }
            bool result = await _customerRepository.Update(customer);
            if (result)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using WebApplication.Models;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Library.Repository;

namespace WebApplication.Pages.Customers
{
    public class IndexCustomer : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public IndexCustomer(IMapper mapper, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public IList<CustomerViewModel> Customer { get;set; }

        public async Task<IActionResult> OnGetAsync()
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
            Customer = _mapper.Map<List<CustomerViewModel>>(await _customerRepository.GetMany());
            return Page();
        }
    }
}

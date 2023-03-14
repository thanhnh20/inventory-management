using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Model;
using DataAccess.Repository;
using WebApplication.Models;
using AutoMapper;

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
            return Page();
        }

        [BindProperty]
        public CustomerViewModel Customer { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customer = _mapper.Map<Customer>(Customer);
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

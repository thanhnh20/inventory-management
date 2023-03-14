﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using DataAccess.Repository;
using AutoMapper;
using WebApplication.Models;

namespace WebApplication.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public DeleteModel(IMapper mapper, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [BindProperty]
        public CustomerViewModel Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _customerRepository.GetOne(s => s.CustomerId == id);
            if (customer == null)
            {
                return Page();
            }
            bool result = await _customerRepository.Delete(customer);
            if (result)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

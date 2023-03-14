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

namespace WebApplication.Pages.Suppliers
{
    public class CreateModel : PageModel
    {
        private readonly ISuplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public CreateModel(IMapper mapper, ISuplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SupplierViewModel Suplier { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var supplier = _mapper.Map<Suplier>(Suplier);
            if(supplier != null)
            {
                bool result = await _supplierRepository.Add(supplier);
                if(result)
                {
                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }
    }
}

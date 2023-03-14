using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using DataAccess.Repository;
using WebApplication.Models;
using AutoMapper;

namespace WebApplication.Pages.Suppliers
{
    public class EditModel : PageModel
    {
        private readonly ISuplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public EditModel(IMapper mapper, ISuplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        [BindProperty]
        public SupplierViewModel Suplier { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("./Index");
            }

            Suplier = _mapper.Map<SupplierViewModel>(await _supplierRepository.GetOne(s => s.SuplierId == id));
            if (Suplier == null)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var supplier = _mapper.Map<Suplier>(Suplier);
            if (supplier == null)
            {
                return Page();
            }
            bool result = await _supplierRepository.Update(supplier);
            if (result)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

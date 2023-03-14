using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using DataAccess.Repository;
using WebApplication.Models;
using AutoMapper;

namespace WebApplication.Pages.Suppliers
{
    public class DeleteModel : PageModel
    {
        private readonly ISuplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public DeleteModel(IMapper mapper, ISuplierRepository supplierRepository)
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
                return NotFound();
            }
            Suplier = _mapper.Map<SupplierViewModel>(await _supplierRepository.GetOne(s => s.SuplierId == id));

            if (Suplier == null)
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
            var supplier = await _supplierRepository.GetOne(s => s.SuplierId == id);
            if(supplier == null)
            {
                return Page();
            }
            bool result = await _supplierRepository.Delete(supplier);
            if (result)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}

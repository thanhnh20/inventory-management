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
            var supplier = await _supplierRepository.GetOne(sup => sup.SuplierName.ToLower().Equals(Suplier.SuplierName.ToLower()));
            if (supplier != null)
            {
                ViewData["Error"] = "Already exist supplier name!";
                return Page();
            }

            supplier = await _supplierRepository.GetOne(sup => sup.SuplierPhone.ToLower().Equals(Suplier.SuplierPhone.ToString().ToLower()));
            if (supplier != null)
            {
                ViewData["Error"] = "Already exist Phone number!";
                return Page();
            }
            */
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

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
        public SupplierViewModel Suplier { get; set; }

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

            var supplier = _mapper.Map<Suplier>(Suplier);
            if (supplier != null)
            {
                bool result = await _supplierRepository.Add(supplier);
                if (result)
                {
                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }
    }
}

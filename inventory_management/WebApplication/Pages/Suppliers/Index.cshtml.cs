﻿using System;
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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Pages.Suppliers
{
    public class IndexModel : PageModel
    {
        private readonly ISuplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public IndexModel(IMapper mapper, ISuplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public IList<SupplierViewModel> Suplier { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var accountJson = HttpContext.Session.GetString("ADMIN");
            if (string.IsNullOrEmpty(accountJson))
            {
                return RedirectToPage("../AdminPages/MainPage");
            }
            var account = JsonConvert.DeserializeObject<User>(accountJson);
            if (account == null)
            {
                return RedirectToPage("../AdminPages/MainPage");
            }
            Suplier = _mapper.Map<List<SupplierViewModel>>(await _supplierRepository.GetMany());
            return Page();
        }
    }
}

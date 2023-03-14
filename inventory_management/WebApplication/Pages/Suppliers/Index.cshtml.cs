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

        public async Task OnGetAsync()
        {
            Suplier = _mapper.Map<List<SupplierViewModel>>(await _supplierRepository.GetMany());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using WebApplication.Models;

namespace WebApplication.Pages.AdminPages
{
    public class RevenueDetailModel : PageModel
    {
        private readonly Library.Model.InventoryManagementContext _context;

        public RevenueDetailModel(Library.Model.InventoryManagementContext context)
        {
            _context = context;
        }

        public IList<RevenueDetailViewModel> RevenueDetails { get;set; }

        public async Task OnGetAsync()
        {
        }
    }
}

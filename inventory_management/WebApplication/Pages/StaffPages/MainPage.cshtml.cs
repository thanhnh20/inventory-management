using Library.Model;
using Library.Repository.RepositoryImpl;
using Library.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Linq;

namespace WebApplication.Pages.StaffPages
{
    public class MainPageModel : PageModel
    {

        private readonly ILogger _logger;
        public string Error { get; set; }
        private readonly InventoryManagementContext _context;
        public ProductRepo productRepo;
        private static readonly string _urlHomePage = "~/HomePages/Home";
        public MainPageModel(ILogger<MainPageModel> logger)
        {
            productRepo = new ProductRepoI();
            _context = new InventoryManagementContext();
            _logger = logger;
        }

        public IList<Product> Product { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("STAFF"));
                var products = productRepo.GetAll();
                Product = products.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " at MainPageModel");
                Error = ex.Message;

            }
            return Page();
        }
        public IActionResult OnGetLogOut()
        {
            HttpContext.Session.Remove("STAFF");
            return Redirect(_urlHomePage);
            /*
        public IActionResult OnGet()
        {
            return RedirectToPage("../Products/Statistic");
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("STAFF");
            return Redirect("~/HomePages/Home");
        }

        public IActionResult OnGetLogin()
        {
            return Redirect("~/HomePages/Home");*/
        }
    }
}


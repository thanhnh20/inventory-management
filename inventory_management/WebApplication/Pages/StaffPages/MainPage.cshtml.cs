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
        public IProductRepository productRepo;
        private static readonly string _urlHomePage = "~/HomePages/Home";
        public MainPageModel(ILogger<MainPageModel> logger, IProductRepository productRepo)
        {
            this.productRepo = productRepo;
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public string? searchString { get; set; }
        public IList<Product> Product { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("STAFF"));
                Product = productRepo.GetProducts();
                TotalPages = (int)Math.Ceiling(Product.Count / (double)6); // assuming 10 items per page
                Product = Product.Skip((PageIndex - 1) * 6).Take(6).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " at MainPageModel");
                Error = ex.Message;
                return Redirect("~/HomePages/Home");
            }
            return Page();
        }

        public void OnPost()
        {
            if (searchString != null)
            {
                var list = productRepo.GetAllAndDescending(searchString);
                Product = list.ToList();
                TotalPages = (int)Math.Ceiling(Product.Count / (double)6); // assuming 10 items per page
                Product = Product.Skip((PageIndex - 1) * 6).Take(6).ToList();
                
            }
            else
            {
                var list = productRepo.GetAll();
                Product = list.ToList();
                TotalPages = (int)Math.Ceiling(Product.Count / (double)6); // assuming 10 items per page
                Product = Product.Skip((PageIndex - 1) * 6).Take(6).ToList();               
            }
            
        }

        public IActionResult OnGetLogOut()
        {
            HttpContext.Session.Remove("STAFF");
            HttpContext.Session.Remove("LIST_INVOICE_INPUT");
            HttpContext.Session.Remove("LIST_INVOICE_OUTPUT");
            return Redirect(_urlHomePage);
        }
        public IActionResult OnGetLogin()
        {
            return Redirect("~/HomePages/Home");
        }
    }

}


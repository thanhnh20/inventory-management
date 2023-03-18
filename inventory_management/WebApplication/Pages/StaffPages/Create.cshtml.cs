using AutoMapper;
using Library.Model;
using Library.Repository;
using Library.Repository.RepositoryImpl;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebApplication.Pages.StaffPages
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryRepository categoryRepo;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public List<Category> categories { get; set; }
        public string Error { get; set; }
        private IProductRepository _productRepo;
        public CreateModel(ILogger<MainPageModel> logger, IWebHostEnvironment webHost, ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            webHostEnvironment = webHost;
            _productRepo = productRepository;
            _logger = logger;
            this.categoryRepo = categoryRepository;
            var listCategory = categoryRepo.GetAll();
            categories = listCategory.ToList();
        }
        [BindProperty]
        public Category Category { get; set; }
    
        public IActionResult OnGet()
        {
           
            try
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("STAFF"));
                return Page();
            }catch (Exception ex)
            {

                _logger.LogError(ex.Message + " at MainPageModel");
                Error = ex.Message;

            }
            return Page();
        }
        private List<SelectListItem> GetCategories()
        {
            var lstCategories = new List<SelectListItem>();
            lstCategories = (List<SelectListItem>) categoryRepo.GetAll();
            var dmyItem = new SelectListItem()
            {
                Value = null,
                Text = "---Select Category---"
            };
            return lstCategories;
        }
        [BindProperty]
        public Product Product { get; set; }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string uniqueFileName = UploadedFile();
            Product.Image = uniqueFileName;
            Product.TotalQuantity = 0;
             _productRepo.Add(Product);
            

            return RedirectToPage("./MainPage");
        }
        private string UploadedFile()
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            string uniqueFileName = null;
            if (Product.FrontImage != null)
            {
                uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Product.FrontImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Product.FrontImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}

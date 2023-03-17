using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using Library.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using Library.Repository.RepositoryImpl;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebApplication.Pages.StaffPages
{
    public class EditModel : PageModel
    {
        private readonly Library.Model.InventoryManagementContext _context;
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger _logger;
        public string Error { get; set; }
        public EditModel(IMapper mapper, ICategoryRepository categoryRepository, IProductRepository productRepository
            , ILogger<MainPageModel> logger, IWebHostEnvironment webHost) {
            this.categoryRepository = categoryRepository;
            _mapper = mapper;
            _context = new InventoryManagementContext();
            this.productRepository = productRepository;
            var listCategory = categoryRepository.GetAll();
            categories = listCategory.ToList();
            webHostEnvironment = webHost;
            _mapper= mapper;
            _logger = logger;
        }

        [BindProperty]
        public Product Product { get; set; }
        public List<Category> categories { get; set; }

        public IActionResult OnGet(int id)
        {

            try
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("STAFF"));
                Product = productRepository.GetProductById(id);
                return Page();
                
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message + " at MainPageModel");
                Error = ex.Message;

            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public  IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Product != null)
            {
                string uniqueFileName = UploadedFile();
                Product.Image = uniqueFileName;
                productRepository.UpdateProduct(Product);
            }
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

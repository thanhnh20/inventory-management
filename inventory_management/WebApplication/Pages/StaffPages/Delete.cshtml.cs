using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using AutoMapper;
using Library.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApplication.Pages.StaffPages
{
    public class DeleteModel : PageModel
    {
        private readonly Library.Model.InventoryManagementContext _context;

        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger _logger;
        public string Error { get; set; }
        public DeleteModel(IMapper mapper, ICategoryRepository categoryRepository, IProductRepository productRepository
            , ILogger<MainPageModel> logger, IWebHostEnvironment webHost)
        {
            this.categoryRepository = categoryRepository;
            _mapper = mapper;
            _context = new InventoryManagementContext();
            this.productRepository = productRepository;
            var listCategory = categoryRepository.GetAll();
            
            webHostEnvironment = webHost;
            _mapper = mapper;
        }
      

        [BindProperty]
        public Product Product { get; set; }

        public  IActionResult OnGet(int id)
        {
       

            Product = productRepository.GetProductById(id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            
            
                productRepository.DeleteProductByID(Product);
            

            return RedirectToPage("./MainPage");
        }
    }
}

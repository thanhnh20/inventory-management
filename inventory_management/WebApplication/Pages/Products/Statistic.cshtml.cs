using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using WebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Library.Repository;
using Library.Utils;

namespace WebApplication.Pages.Products
{
    public class StatisticModel : PageModel
    {
        private readonly IConsignmentRepository _consignmentRepo;
        private readonly IConsignmentDetailRepository _consignmentDetailRepo;
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }

        public StatisticModel(IConsignmentRepository consignmentRepo, IConsignmentDetailRepository consignmentDetailRepo, IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _consignmentRepo = consignmentRepo;
            _consignmentDetailRepo = consignmentDetailRepo;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public IList<ProductStatisticViewModel> Product { get; set; }

        public IEnumerable<SelectListItem> Consignments { get; set; }

        public async Task<IActionResult> OnGetAsync()
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
            Consignments = (await _consignmentRepo.GetMany()).Select(c => new SelectListItem()
            {
                Text = c.ConsignmentName,
                Value = c.ConsignmentId.ToString()
            });
            Product = new List<ProductStatisticViewModel>();
            var consignments = await _consignmentRepo.GetMany();
            if (consignments != null && consignments.Count() > 0)
            {
                foreach (var consignment in consignments)
                {
                    if(consignment.Status == (int)StatusType.IsDeleted)
                    {
                        continue;
                    }
                    var consignmentDetails = await _consignmentDetailRepo.GetMany(cd => cd.ConsignmentId == consignment.ConsignmentId);
                    if(consignmentDetails == null || consignmentDetails.Count() == 0)
                    {
                        continue;
                    }
                    foreach (var consignmentDetail in consignmentDetails)
                    {
                        var product = await _productRepo.GetOne(p => p.ProductId == consignmentDetail.ProductId);
                        if(product == null)
                        {
                            continue;
                        }
                        if (product.Status == (int)StatusType.IsDeleted)
                        {
                            continue;
                        }
                        var category = await _categoryRepo.GetOne(c => c.CategoryId == product.CategoryId);
                        if (category == null)
                        {
                            continue;
                        }
                        Product.Add(new ProductStatisticViewModel()
                        {
                            ProductName = product.ProductName,
                            CategoryName = category.CategoryName,
                            ConsignmentName = consignment.ConsignmentName,
                            Description = product.Description,
                            Image = product.Image,
                            ImportPrice = product.ImportPrice.Value,
                            Quantity = consignmentDetail.Quantity.Value,
                            SellingPrice = product.SellingPrice.Value,
                            Unit = product.Unit,
                        });
                    }                   
                }
            }
            TotalPages = (int)Math.Ceiling(Product.Count / (double)4); // assuming 10 items per page
            Product = Product.Skip((PageIndex - 1) * 4).Take(4).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int consignmentId)
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
            if (consignmentId == -1)
            {
                return RedirectToPage("../Products/Statistic");
            }
            else
            {
                Consignments = (await _consignmentRepo.GetMany()).Select(c => new SelectListItem()
                {
                    Text = c.ConsignmentName,
                    Value = c.ConsignmentId.ToString()
                });
                Product = new List<ProductStatisticViewModel>();
                var consignment = await _consignmentRepo.GetOne(c => c.ConsignmentId == consignmentId);
                if (consignment == null)
                {
                    return Page();
                }
                if (consignment.Status == (int)StatusType.IsDeleted)
                {
                    return Page();
                }
                var consignmentDetails = await _consignmentDetailRepo.GetMany(cd => cd.ConsignmentId == consignmentId);
                if (consignmentDetails == null || consignmentDetails.Count() == 0)
                {
                    return Page();
                }
                foreach (var consignmentDetail in consignmentDetails)
                {
                    var product = await _productRepo.GetOne(p => p.ProductId == consignmentDetail.ProductId);
                    if (product == null)
                    {
                        continue;
                    }
                    if (product.Status == (int)StatusType.IsDeleted)
                    {
                        continue;
                    }
                    var category = await _categoryRepo.GetOne(c => c.CategoryId == product.CategoryId);
                    if (category == null)
                    {
                        continue;
                    }
                    Product.Add(new ProductStatisticViewModel()
                    {
                        ProductName = product.ProductName,
                        CategoryName = category.CategoryName,
                        ConsignmentName = consignment.ConsignmentName,
                        Description = product.Description,
                        Image = product.Image,
                        ImportPrice = product.ImportPrice.Value,
                        Quantity = consignmentDetail.Quantity.Value,
                        SellingPrice = product.SellingPrice.Value,
                        Unit = product.Unit,
                    });
                }
                return Page();
            }
        }
    }
}

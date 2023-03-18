using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using WebApplication.Models;
using Library.Repository;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace WebApplication.Pages.AdminPages
{
    public class RevenueDetailModel : PageModel
    {
        private readonly IInvoiceOutputRepository _invoiceOutputRepo;
        private readonly IInvoiceOutputDetailRepository _invoiceOutputDetailRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IUserRepository _userRepo;
        private readonly IProductRepository _productRepo;
        private readonly IConsignmentDetailRepository _consignmentDetailRepo;
        private readonly IConsignmentRepository _consignmentRepo;
        private readonly ICategoryRepository _categoryRepo;

        public RevenueDetailModel(IInvoiceOutputRepository invoiceOutputRepo,
            IConsignmentRepository consignmentRepo, IConsignmentDetailRepository consignmentDetailRepo,
            IInvoiceOutputDetailRepository invoiceOutputDetailRepo, IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _invoiceOutputDetailRepo = invoiceOutputDetailRepo;
            _invoiceOutputRepo = invoiceOutputRepo;
            _productRepo = productRepo;
            _consignmentDetailRepo = consignmentDetailRepo;
            _consignmentRepo = consignmentRepo;
            _categoryRepo = categoryRepo;
        }

        public IList<RevenueDetailViewModel> RevenueDetails { get;set; }

        public int OutputBillId { get; set; }

        public async Task<IActionResult> OnGetAsync(int outputBillId)
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
            OutputBillId = outputBillId;
            await GetRevenueModel(outputBillId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string type, string search, int outputBillId)
        {
            OutputBillId = outputBillId;
            if (string.IsNullOrEmpty(search))
            {
                await GetRevenueModel(outputBillId);
                return Page();
            }
            await GetRevenueModel(outputBillId);
            if (RevenueDetails == null || RevenueDetails.Count == 0)
            {
                return Page();
            }
            if (type.Equals("consignmentName"))
            {
                RevenueDetails = RevenueDetails.Where(r => r.ConsignmentName.ToLower().Contains(search.ToLower().Trim())).ToList();
                if (RevenueDetails == null || RevenueDetails.Count == 0)
                {
                    RevenueDetails = new List<RevenueDetailViewModel>();
                }
            }
            else if (type.Equals("productName"))
            {
                RevenueDetails = RevenueDetails.Where(r => r.ProductName.ToLower().Contains(search.ToLower().Trim())).ToList();
                if (RevenueDetails == null || RevenueDetails.Count == 0)
                {
                    RevenueDetails = new List<RevenueDetailViewModel>();
                }
            }
            else if (type.Equals("categoryName"))
            {
                RevenueDetails = RevenueDetails.Where(r => r.CategoryName.ToLower().Contains(search.ToLower().Trim())).ToList();
                if (RevenueDetails == null || RevenueDetails.Count == 0)
                {
                    RevenueDetails = new List<RevenueDetailViewModel>();
                }
            }
            return Page();
        }

        private async Task<bool> GetRevenueModel(int outputBillId)
        {
            var outputDetails = await _invoiceOutputDetailRepo.GetMany(invoiceDetail => invoiceDetail.OutputBillId == outputBillId);
            if (outputDetails == null || outputDetails.Count() == 0)
            {
                return false;
            }
            var nList = new List<RevenueDetailViewModel>();
            foreach (var outputDetail in outputDetails)
            {
                var consignmentDetail = await _consignmentDetailRepo.GetOne(cd => cd.ConsignmentDetailId == outputDetail.ConsignmentDetailId);
                if (consignmentDetail == null)
                {
                    RevenueDetails = nList;
                    return false;
                }
                var consignment = await _consignmentRepo.GetOne(c => c.ConsignmentId == consignmentDetail.ConsignmentId);
                if (consignment == null)
                {
                    RevenueDetails = nList;
                    return false;
                }
                var product = await _productRepo.GetOne(p => p.ProductId == consignmentDetail.ProductId);
                if (product == null)
                {
                    RevenueDetails = nList;
                    return false;
                }
                var category = await _categoryRepo.GetOne(c => c.CategoryId == product.CategoryId);
                if (category == null)
                {
                    RevenueDetails = nList;
                    return false;
                }
                try
                {
                    nList.Add(new RevenueDetailViewModel()
                    {
                        CategoryName = category.CategoryName,
                        QuantityLeft = consignmentDetail.Quantity.Value,
                        ConsignmentName = consignment.ConsignmentName,
                        Image = product.Image,
                        ProductName = product.ProductName,
                        QuantityExported = outputDetail.Quantity,
                        SellingPrice = product.SellingPrice.Value,
                        TotalPrice = outputDetail.TotalPrice,
                        Unit = product.Unit,
                    });
                }
                catch (Exception ex)
                {
                    RevenueDetails = nList;
                    return false;
                }
            }
            RevenueDetails = nList;
            return true;
        }
    }
}

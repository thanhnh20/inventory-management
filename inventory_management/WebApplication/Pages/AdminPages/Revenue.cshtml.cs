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
using ClosedXML.Excel;
using System.IO;

namespace WebApplication.Pages.AdminPages
{
    public class RevenueModel : PageModel
    {
        private readonly IInvoiceOutputRepository _invoiceOutputRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IUserRepository _userRepo;
        private readonly IInvoiceOutputDetailRepository _invoiceOutputDetailRepo;
        private readonly IConsignmentDetailRepository consignmentDetailRepository;

        public RevenueModel(IInvoiceOutputRepository invoiceOutputRepo, 
            ICustomerRepository customerRepo, IUserRepository userRepo, IInvoiceOutputDetailRepository invoiceOutputDetailRepo, IConsignmentDetailRepository consignmentDetailRepository)
        {
            _invoiceOutputRepo = invoiceOutputRepo;
            _customerRepo = customerRepo;
            _userRepo = userRepo;
            _invoiceOutputDetailRepo = invoiceOutputDetailRepo;
            this.consignmentDetailRepository = consignmentDetailRepository;
        }

        public IList<RevenueViewModel> Revenues { get;set; }

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
            await GetRevenueModel();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string type, string search)
        {
            if(string.IsNullOrEmpty(search))
            {
                await GetRevenueModel();
                return Page();
            }
            await GetRevenueModel();
            if (Revenues == null || Revenues.Count == 0)
            {
                return Page();
            }
            if(type.Equals("exporterName"))
            {
                Revenues = Revenues.Where(r => r.ExporterName.ToLower().Contains(search.ToLower().Trim())).ToList();
                if (Revenues == null || Revenues.Count == 0)
                {
                    Revenues = new List<RevenueViewModel>();
                }
            } else if(type.Equals("customerName"))
            {
                Revenues = Revenues.Where(r => r.CustomerName.ToLower().Contains(search.ToLower().Trim())).ToList();
                if (Revenues == null || Revenues.Count == 0)
                {
                    Revenues = new List<RevenueViewModel>();
                }
            }
            return Page();
        }

        private async Task<bool> GetRevenueModel()
        {
            var invoiceOutputs = await _invoiceOutputRepo.GetMany();
            var revenues = new List<RevenueViewModel>();
            if (invoiceOutputs == null || invoiceOutputs.Count() == 0)
            {
                Revenues = revenues;
                return false;
            }
            foreach (var invoiceOutput in invoiceOutputs)
            {
                var customer = await _customerRepo.GetOne(cus => cus.CustomerId == invoiceOutput.CustomerId);
                if (customer == null)
                {
                    continue;
                }
                var user = await _userRepo.GetOne(u => u.UserId == invoiceOutput.UserId) as User;
                if (user == null)
                {
                    continue;
                }
                var invoiceOutputDetails = await _invoiceOutputDetailRepo.GetMany(invoiceD => invoiceD.OutputBillId == invoiceOutput.OutputBillId);
                double totalPrice = 0;
                if (invoiceOutputDetails != null && invoiceOutputDetails.Count() > 0)
                {
                    foreach (var invoiceOutputDetail in invoiceOutputDetails)
                    {
                        if (invoiceOutputDetail.TotalPrice != null)
                        {
                            totalPrice += invoiceOutputDetail.TotalPrice.Value;
                        }
                    }
                    revenues.Add(new RevenueViewModel()
                    {
                        CustomerName = customer.CustomerName,
                        OutputDate = invoiceOutput.OutputDate,
                        Amount = invoiceOutput.Amount,
                        OutputBillId = invoiceOutput.OutputBillId,
                        ExporterName = user.FullName,
                        TotalPrice = totalPrice,
                    });
                }
            }
            Revenues = revenues;
            return true;
        }

        public IActionResult OnGetExportExcel()
        {
            var accountJson = HttpContext.Session.GetString("ADMIN");
            if (string.IsNullOrEmpty(accountJson))
            {
                return Redirect("~/HomePages/Home");
            }
            else
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if (AccountSession == null)
                {
                    return Redirect("~/HomePages/Home");
                }
                else
                {
                    //export to excel
                    //var data = invoiceInputRepository.ListAll();
                    var a = consignmentDetailRepository.GetConsignmentDetailsOutput();

                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet = workbook.Worksheets.Add("InvoiceInput");
                        worksheet.Cell(1, 1).Value = "Bill ID";
                        worksheet.Cell(1, 2).Value = "Customer";
                        worksheet.Cell(1, 3).Value = "Staff in charge";
                        worksheet.Cell(1, 4).Value = "Date";
                        worksheet.Cell(1, 5).Value = "Consignment ID";
                        worksheet.Cell(1, 6).Value = "Product";
                        worksheet.Cell(1, 7).Value = "Category";
                        worksheet.Cell(1, 8).Value = "Selling Price";
                        worksheet.Cell(1, 9).Value = "Quantity Export";
                        worksheet.Cell(1, 10).Value = "Total Price";

                        IXLRange range = worksheet.Range(worksheet.Cell(1, 1).Address, worksheet.Cell(1, 10).Address);
                        range.Style.Fill.SetBackgroundColor(XLColor.BlueBell);

                        int index = 1;
                        
                        foreach (var item in a)
                        {
                            index++;
                            foreach (var o in item.InvoiceOutputDetails)
                            {
                                worksheet.Cell(index, 1).Value = o.OutputBill.OutputBillId;
                                worksheet.Cell(index, 2).Value = o.OutputBill.Customer.CustomerName;
                                var user = _userRepo.GetUserByID(o.OutputBill.UserId);
                                worksheet.Cell(index, 3).Value = user.FullName;
                                worksheet.Cell(index, 4).Value = o.OutputBill.OutputDate;
                                worksheet.Cell(index, 5).Value = o.ConsignmentDetail.ConsignmentId;
                                worksheet.Cell(index, 6).Value = o.ConsignmentDetail.Product.ProductName;
                                worksheet.Cell(index, 7).Value = o.ConsignmentDetail.Product.Category.CategoryName;
                                worksheet.Cell(index, 8).Value = o.ConsignmentDetail.Product.SellingPrice;

                                var invoiceOutputDetail = _invoiceOutputDetailRepo.GetOne(i => i.OutputDetailId == o.OutputDetailId);
                                worksheet.Cell(index, 9).Value = invoiceOutputDetail.Result.Quantity;

                                var amount = o.ConsignmentDetail.Product.SellingPrice * invoiceOutputDetail.Result.Quantity;
                                var amountFormat = string.Format("{0:#,##0}", amount);
                                worksheet.Cell(index, 10).Value = amountFormat;
                            }

                        }

                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            string contenType = "application/vnd.openxmlformats-officedocument.spreadsheettml.sheet";
                            var strDate = DateTime.Now.ToString("yyyyMMdd");
                            string fileName = string.Format($"InvoiceOutput_{strDate}.xlsx");

                            return File(content, contenType, fileName);
                        }
                    }
                }
            }
        }
    }
}

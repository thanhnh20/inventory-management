using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Model;
using Library.Repository;
using Library.Repository.RepositoryImpl;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.IO;
using Library.Utils;

namespace WebApplication.Pages.AdminPages
{
    public class InvoiceInputReportModel : PageModel
    {
        public string Error { get; set; }

        IInvoiceInputRepository invoiceInputRepository;
        IConsignmentDetailRepository consignmentDetailRepository;
        IInvoiceInputDetailRepository invoiceInputDetailRepository;
        IUserRepository userRepository; 

        public InvoiceInputReportModel(IConsignmentDetailRepository consignmentDetailRepository, IUserRepository userRepository)
        {
            this.invoiceInputRepository = new InvoiceInputRepository();
            this.consignmentDetailRepository = consignmentDetailRepository;
            this.invoiceInputDetailRepository = new InvoiceInputDetailRepositoryIml();
            this.userRepository = userRepository;
        }

        public IList<InvoiceInput> InvoiceInput { get;set; }

        public IActionResult OnGet()
        {
            var accountJson = HttpContext.Session.GetString("ADMIN");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "accountJson is null";
                return Redirect("~/HomePages/Home");
            }
            else
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if (AccountSession == null)
                {
                    Error = "accountSession is null";
                    return Redirect("~/HomePages/Home");
                }
                else
                {
                    InvoiceInput = invoiceInputRepository.ListAll();    
                }
            }
            return Page();
        }

        public IActionResult OnGetExportExcel()
        {
            var accountJson = HttpContext.Session.GetString("ADMIN");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "accountJson is null";
                return Redirect("~/HomePages/Home");
            }
            else
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if (AccountSession == null)
                {
                    Error = "accountSession is null";
                    return Redirect("~/HomePages/Home");
                }
                else
                {
                    //export to excel
                    var data = invoiceInputRepository.ListAll();
                    var a = consignmentDetailRepository.GetConsignmentDetails();        

                    using (var workbook = new XLWorkbook())
                    {
                        IXLWorksheet worksheet = workbook.Worksheets.Add("InvoiceInput");
                        worksheet.Cell(1, 1).Value = "Bill ID";
                        worksheet.Cell(1, 2).Value = "Suplier";
                        worksheet.Cell(1, 3).Value = "Staff in charge";
                        worksheet.Cell(1, 4).Value = "Date";
                        worksheet.Cell(1, 5).Value = "Consignment ID";
                        worksheet.Cell(1, 6).Value = "Product";
                        worksheet.Cell(1, 7).Value = "Category";
                        worksheet.Cell(1, 8).Value = "Import Price";
                        worksheet.Cell(1, 9).Value = "Quantity Import";
                        worksheet.Cell(1, 10).Value = "Total Price";

                        IXLRange range = worksheet.Range(worksheet.Cell(1, 1).Address, worksheet.Cell(1, 10).Address);
                        range.Style.Fill.SetBackgroundColor(XLColor.CambridgeBlue);

                        int index = 1;

                        foreach (var item in a)
                        {
                            index++;
                            foreach (var o in item.InvoiceInputDetails)
                            {
                                
                                worksheet.Cell(index, 1).Value = o.InputBill.InputBillId;
                                worksheet.Cell(index, 2).Value = o.InputBill.Suplier.SuplierName;
                                var user = userRepository.GetUserByID(o.InputBill.UserId);
                                worksheet.Cell(index, 3).Value = user.FullName;
                                worksheet.Cell(index, 4).Value = o.InputBill.InputDate;
                                worksheet.Cell(index, 5).Value = o.ConsignmentDetail.ConsignmentId;
                                worksheet.Cell(index, 6).Value = o.ConsignmentDetail.Product.ProductName;
                                worksheet.Cell(index, 7).Value = o.ConsignmentDetail.Product.Category.CategoryName;
                                worksheet.Cell(index, 8).Value = o.ConsignmentDetail.Product.ImportPrice;
                                var invoiceInputDetail = invoiceInputDetailRepository.getInvoiceInputDetailById(o.InputDetailId);
                                worksheet.Cell(index, 9).Value = invoiceInputDetail.Quantity;

                                var amount = o.ConsignmentDetail.Product.ImportPrice * invoiceInputDetail.Quantity;
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
                            string fileName = string.Format($"InvoiceInput_{strDate}.xlsx");

                            return File(content, contenType, fileName);
                        }
                    }
                }
            }           
        }
    }
}

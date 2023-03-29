using Library.Model;
using Library.Repository;
using Library.Repository.RepositoryImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Pages.StaffPages
{
    public class CreateInvoiceOutputModel : PageModel
    {
        private readonly ILogger _logger;

        public string Error { get; set; }

        public string Msg { get; set; }

        public string msgErrorQuantity { get; set; }

        public string msgWarningQuantity { get; set; }

        [BindProperty]
        public InvoiceOutput InvoiceOutput { get; set; }

        [BindProperty]
        public Consignment Consignment { get; set; }

        [BindProperty]
        public ConsignmentDetail ConsignmentDetail { get; set; }

        public List<Product> Products { get; set; }


        [BindProperty]
        public Product Product { get; set; }

        ICustomerRepository customerRepository;

        IInvoiceOutputRepository invoiceOutputRepository;

        IProductRepository productRepository;

        IConsignmentDetailRepository consignmentDetailRepository;

        public CreateInvoiceOutputModel(ILogger<CreateInvoiceOutputModel> logger, ICustomerRepository customerRepository, IProductRepository productRepository, IConsignmentDetailRepository consignmentDetailRepository)
        {
            this.consignmentDetailRepository = consignmentDetailRepository;
            invoiceOutputRepository = new InvoiceOutputRepositoryIml();
            this.productRepository = productRepository;
            this.customerRepository = customerRepository;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            var accountJson = HttpContext.Session.GetString("STAFF");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "accountJson is null";
                return Redirect("~/HomePages/Home");
            }
            else
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if (accountJson == null)
                {
                    Error = "accountSession is null";
                    return Redirect("~/HomePages/Home");
                }
                else
                {
                    try
                    {
                        var ListInvoiceOutput = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("LIST_INVOICE_OUTPUT"));
                        Products = ListInvoiceOutput.ToList();
                        ViewData["CustomerName"] = new SelectList(customerRepository.GetAll(), "CustomerId", "CustomerName");
                        if (ListInvoiceOutput.Count > 0)
                        {
                            foreach (var Product in ListInvoiceOutput)
                            {
                                ViewData[Product.ProductId + ""] = consignmentDetailRepository.getConsignmentIDByProductID(Product.ProductId);
                                ConsignmentDetail = Product.ConsignmentDetails.FirstOrDefault();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Msg = "List Invoice input is null";
                    }
                }
            }
            return Page();
        }

        public IActionResult OnGetAddInvoiceOutput(int ProductId)
        {
            var accountJson = HttpContext.Session.GetString("STAFF");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "Account Json is null";
                return Redirect("~/HomePages/Home");
            }
            else
            {
                var accountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if (accountSession == null)
                {
                    Error = "Account Session is null";
                    return Redirect("~/HomePages/Home");
                }
                else
                {
                    try
                    {
                        var listInvoiceOutput = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("LIST_INVOICE_OUTPUT"));
                        if (listInvoiceOutput != null)
                        {
                            var product = productRepository.GetProductById(ProductId);
                            var productAdd = new Product()
                            {
                                ProductId = product.ProductId,
                                ProductName = product.ProductName,
                                Category = product.Category,
                                CategoryId = product.CategoryId,
                                Status = product.Status,
                                TotalQuantity = product.TotalQuantity,
                                FrontImage = product.FrontImage,
                                Image = product.Image,
                                ImportPrice = product.ImportPrice,
                            };
                            product.Category.Products = null;
                            var productCheck = listInvoiceOutput.Where(x => x.ProductId == ProductId).FirstOrDefault();
                            if (productCheck != null)
                            {
                                ++listInvoiceOutput.Where(x => x.ProductId == ProductId).FirstOrDefault().TotalQuantity;
                            }
                            else
                            {
                                productAdd.TotalQuantity = 1;
                                listInvoiceOutput.Add(productAdd);
                            }

                        }
                        HttpContext.Session.SetString("LIST_INVOICE_OUTPUT", JsonConvert.SerializeObject(listInvoiceOutput));
                    }
                    catch (Exception ex)
                    {
                        var listInvoiceOutput = new List<Product>();
                        var product = productRepository.GetProductById(ProductId);
                        var productAdd = new Product()
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            Category = product.Category,
                            CategoryId = product.CategoryId,
                            Status = product.Status,
                            TotalQuantity = product.TotalQuantity,
                            FrontImage = product.FrontImage,
                            Image = product.Image,
                            ImportPrice = product.ImportPrice,
                        };
                        product.Category.Products = null;
                        productAdd.TotalQuantity = 1;
                        listInvoiceOutput.Add(productAdd);
                        HttpContext.Session.SetString("LIST_INVOICE_OUTPUT", JsonConvert.SerializeObject(listInvoiceOutput));
                    }
                }
            }
            return RedirectToPage("MainPage");
        }

        public IActionResult OnGetRemoveProductInWaitInvoice(int ProductId)
        {
            var accountJson = HttpContext.Session.GetString("STAFF");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "Account Json is null";
                return Redirect("~/HomePages/Home");
            }
            else
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if (accountJson == null)
                {
                    Error = "Account Session is null";
                    return Redirect("~/HomePages/Home");
                }
                else
                {
                    try
                    {
                        var ListInvoiceOutput = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("LIST_INVOICE_OUTPUT"));
                        var product = ListInvoiceOutput.Where(m => m.ProductId == ProductId).FirstOrDefault();
                        ListInvoiceOutput.Remove(product);
                        HttpContext.Session.SetString("LIST_INVOICE_OUTPUT", JsonConvert.SerializeObject(ListInvoiceOutput));
                        Products = ListInvoiceOutput.ToList();

                        ViewData["CustomerName"] = new SelectList(customerRepository.GetAll(), "CustomerId", "CustomerName");
                        if (ListInvoiceOutput.Count > 0)
                        {
                            foreach (var Product in ListInvoiceOutput)
                            {
                                ViewData[Product.ProductId + ""] = consignmentDetailRepository.getConsignmentIDByProductID(Product.ProductId);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Msg = "List Invoice output is null";
                    }
                }
            }
            return Page();
        }

        public IActionResult OnPostChangeQuantity()
        {
            var accountJson = HttpContext.Session.GetString("STAFF");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "Account Json is null";
                return Redirect("~/HomePages/Home");
            }
            else
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if (AccountSession == null)
                {
                    Error = "Account Session is null";
                    return Redirect("~/HomePages/Home");
                }
                else
                {
                    try
                    {
                        var ListInvoiceOutput = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("LIST_INVOICE_OUTPUT"));
                        var productCheck = ListInvoiceOutput.Where(x => x.ProductId == Product.ProductId).FirstOrDefault();                       
                        if (productCheck != null)
                        {
                            InvoiceOutput.OutputDate = DateTime.Now;
                            ViewData["CustomerName"] = new SelectList(customerRepository.GetAll(), "CustomerId", "CustomerName");
                            var consignmentDetail = consignmentDetailRepository.getConsignmentByID(ConsignmentDetail.ConsignmentDetailId);
                            if (ListInvoiceOutput.Count > 0)
                            {
                                foreach (var Product in ListInvoiceOutput)
                                {
                                    ViewData[Product.ProductId + ""] = consignmentDetailRepository.getConsignmentIDByProductID(Product.ProductId);
                                }
                            }

                            var productInDB = productRepository.GetProductById(Product.ProductId);
                            bool check = true;
                            if(Product.TotalQuantity > productInDB.TotalQuantity)
                            {
                                msgErrorQuantity = $"The total Product {Product.ProductName} not enough according to your input quantity";
                                check = false;
                            }
                            if (Product.TotalQuantity > consignmentDetail.Quantity)
                            {
                                msgWarningQuantity = $"The quantity Product in consignment have ID {consignmentDetail.ConsignmentId} not enough";
                                check = false;
                            }
                            if (!check)
                            {
                                Products = ListInvoiceOutput.ToList();
                                return Page();
                            }
                            else
                            {                             
                                productCheck.Status = consignmentDetail.ConsignmentId;
                                productCheck.TotalQuantity = Product.TotalQuantity;
                                if(productCheck.ConsignmentDetails.Count == 0)
                                {
                                    productCheck.ConsignmentDetails = new List<ConsignmentDetail>();
                                    productCheck.ConsignmentDetails.Add(consignmentDetail);
                                }
                                else
                                {
                                    productCheck.ConsignmentDetails.FirstOrDefault().ConsignmentDetailId = consignmentDetail.ConsignmentDetailId;
                                    productCheck.ConsignmentDetails.FirstOrDefault().ConsignmentId = consignmentDetail.ConsignmentId;
                                }
                                HttpContext.Session.SetString("LIST_INVOICE_OUTPUT", JsonConvert.SerializeObject(ListInvoiceOutput));
                                Products = ListInvoiceOutput.ToList();
                            }
                            
                        }                                           
                    }
                    catch (Exception ex)
                    {
                        Msg = "List Invoice output is null";
                    }
                }

            }
            return RedirectToPage();
        }

        public IActionResult OnPost()
        {
            var accountJson = HttpContext.Session.GetString("STAFF");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "Account Json is null";
                return Redirect("~/HomePages/Home");
            }
            else
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if (AccountSession == null)
                {
                    Error = "Account Session is null";
                    return Redirect("~/HomePages/Home");
                }
                else
                {
                    try
                    {
                        var ListInvoiceOutput = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("LIST_INVOICE_OUTPUT"));
                        if (ListInvoiceOutput.Count > 0)
                        {        
                            double? amount = 0;
                            bool check = true;
                            foreach (var product in ListInvoiceOutput)
                            {
                                var productInDB = productRepository.GetProductById(product.ProductId);
                                if(product.TotalQuantity > productInDB.TotalQuantity)
                                {
                                    msgErrorQuantity = $"The total Product {product.ProductName} not enough according to your input quantity";
                                    check = false;
                                }
                                var consignmentDetail = consignmentDetailRepository.GetConsignmentDetails(product.ProductId, (int)product.Status);
                                if(product.TotalQuantity > consignmentDetail.Quantity)
                                {
                                    msgErrorQuantity = $"The quantity of Product {product.ProductName} \nin consignment have ID {consignmentDetail.ConsignmentId} not enough";
                                    check = false;
                                }
                                var listConsignmentID = consignmentDetailRepository.getConsignmentIDByProductID(product.ProductId);
                                if(listConsignmentID.Count == 0)
                                {
                                    msgErrorQuantity = $"The total Product {product.ProductName} not enough according to your input quantity";
                                    check = false;
                                }
                                amount = amount + (product.TotalQuantity * product.ImportPrice);
                                if (!check)
                                {
                                    break;
                                }
                            }
                            if (!check)
                            {
                                Products = ListInvoiceOutput.ToList();
                                InvoiceOutput.OutputDate = DateTime.Now;
                                ViewData["CustomerName"] = new SelectList(customerRepository.GetAll(), "CustomerId", "CustomerName");
                                if (ListInvoiceOutput.Count > 0)
                                {
                                    foreach (var Product in ListInvoiceOutput)
                                    {
                                        ViewData[Product.ProductId + ""] = consignmentDetailRepository.getConsignmentIDByProductID(Product.ProductId);
                                    }
                                }
                                return Page();
                            }
                            else
                            {
                                //setup for incoive input
                                InvoiceOutput.UserId = AccountSession.UserId;
                                InvoiceOutput.Amount = amount;

                                //setup for invoice input detail
                                if(invoiceOutputRepository.CreateInvoiceOutput(InvoiceOutput, ListInvoiceOutput))
                                {
                                    HttpContext.Session.Remove("LIST_INVOICE_OUTPUT");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Msg = "List Invoice input is null";
                    }
                }
            }
            return RedirectToPage("MainPage");
        }
    }
}

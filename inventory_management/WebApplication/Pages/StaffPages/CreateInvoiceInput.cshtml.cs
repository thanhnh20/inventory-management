using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Library.Repository;
using Library.Repository.RepositoryImpl;

namespace WebApplication.Pages.StaffPages
{
    public class CreateInvoiceInputModel : PageModel
    {

        private readonly ILogger _logger;

        public string Error { get; set; }

        public string Msg { get; set; }

        [BindProperty]
        public InvoiceInput InvoiceInput { get; set; }

        [BindProperty]
        public Consignment Consignment { get; set; }

        
        IProductRepository productRepository;

        public List<Product> Products { get; set; }

        [BindProperty]
        public Product Product { get; set; }

        ISuplierRepository suplierRepository;

        IInvoiceInputRepository invoiceInputRepository;
        public CreateInvoiceInputModel(ILogger<CreateInvoiceInputModel> logger, ISuplierRepository suplierRepository, IProductRepository productRepository)
        {
            invoiceInputRepository = new InvoiceInputRepository();
            this.productRepository = productRepository;
            this.suplierRepository = suplierRepository;
            _logger = logger;
        }

        public IActionResult OnGet(int ProductId)
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
                if(accountJson == null)
                {
                    Error = "accountSession is null";
                    return Redirect("~/HomePages/Home");
                }
                else
                {
                    try
                    {
                        var ListInvoiceInput = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("LIST_INVOICE_INPUT"));
                        Products = ListInvoiceInput.ToList();
                        ViewData["SuplierName"] = new SelectList(suplierRepository.GetListSuplier(), "SuplierId", "SuplierName");
                    }
                    catch (Exception ex)
                    {
                        Msg = "List Invoice input is null";
                    }
                }
            }               
            return Page();
        }

        public IActionResult OnGetRemoveProductInWaitInvoice(int ProductId)
        {
            var accountJson = HttpContext.Session.GetString("STAFF");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "Account Json is null";
            }
            else
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if(accountJson == null)
                {
                    Error = "Account Session is null";
                }
                else
                {
                    try
                    {
                        var ListInvoiceInput = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("LIST_INVOICE_INPUT"));
                        var product = ListInvoiceInput.Where(m => m.ProductId == ProductId).FirstOrDefault();
                        ListInvoiceInput.Remove(product);
                        HttpContext.Session.SetString("LIST_INVOICE_INPUT", JsonConvert.SerializeObject(ListInvoiceInput));
                        Products = ListInvoiceInput.ToList();

                        ViewData["SuplierName"] = new SelectList(suplierRepository.GetListSuplier(), "SuplierId", "SuplierName");
                    }
                    catch (Exception ex)
                    {
                        Msg = "List Invoice input is null";
                    }
                }
            }
            return Page();
        }

        public IActionResult OnGetAddInvoiceInput(int ProductId)
        {
            var accountJson = HttpContext.Session.GetString("STAFF");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "Account Json is null";
            }
            else
            {
                var accountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if(accountSession == null)
                {
                    Error = "Account Session is null";
                }
                else
                {
                    try
                    {
                        var ListInvoiceInput = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("LIST_INVOICE_INPUT"));
                        if (ListInvoiceInput != null)
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
                            var productCheck = ListInvoiceInput.Where(x => x.ProductId == ProductId).FirstOrDefault();
                            if (productCheck != null)
                            {
                                ++ListInvoiceInput.Where(x => x.ProductId == ProductId).FirstOrDefault().TotalQuantity;
                            }
                            else
                            {
                                productAdd.TotalQuantity = 1;
                                ListInvoiceInput.Add(productAdd);
                            }

                        }
                        HttpContext.Session.SetString("LIST_INVOICE_INPUT", JsonConvert.SerializeObject(ListInvoiceInput));
                    }
                    catch (Exception ex)
                    {
                        var ListInvoiceInput = new List<Product>();
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
                        ListInvoiceInput.Add(productAdd);
                        HttpContext.Session.SetString("LIST_INVOICE_INPUT", JsonConvert.SerializeObject(ListInvoiceInput));
                    }
                }
            }   
            return RedirectToPage("MainPage");               
        }
              
        public IActionResult OnPost()
        {
            var accountJson = HttpContext.Session.GetString("STAFF");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "Account Json is null";
            }
            else
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if(AccountSession == null)
                {
                    Error = "Account Session is null";
                }
                else
                {
                    try
                    {
                        var ListInvoiceInput = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("LIST_INVOICE_INPUT"));
                        if (ListInvoiceInput.Count > 0)
                        {
                            double? amount = 0;
                            foreach (var product in ListInvoiceInput)
                            {
                                amount = amount + (product.TotalQuantity * product.ImportPrice);
                            }
                            //setup for incoive input
                            InvoiceInput.UserId = AccountSession.UserId;
                            InvoiceInput.Amount = amount;

                            //setup for incoive input detail
                            var check = invoiceInputRepository.CreateInvoiceInput(InvoiceInput, Consignment, ListInvoiceInput);
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

        public IActionResult OnPostChangeQuantity()
        {
            var accountJson = HttpContext.Session.GetString("STAFF");
            if (string.IsNullOrEmpty(accountJson))
            {
                Error = "Account Json is null";
            }
            else
            {
                var AccountSession = JsonConvert.DeserializeObject<User>(accountJson);
                if(AccountSession == null)
                {
                    Error = "Account Session is null";
                }
                else
                {
                    try
                    {
                        var ListInvoiceInput = JsonConvert.DeserializeObject<List<Product>>(HttpContext.Session.GetString("LIST_INVOICE_INPUT"));
                        var productCheck = ListInvoiceInput.Where(x => x.ProductId == Product.ProductId).FirstOrDefault();
                        if (productCheck != null)
                        {
                            productCheck.TotalQuantity = Product.TotalQuantity;
                        }
                        InvoiceInput.InputDate = DateTime.Now;
                        HttpContext.Session.SetString("LIST_INVOICE_INPUT", JsonConvert.SerializeObject(ListInvoiceInput));
                        Products = ListInvoiceInput.ToList();
                        ViewData["SuplierName"] = new SelectList(suplierRepository.GetListSuplier(), "SuplierId", "SuplierName");
                    }
                    catch (Exception ex)
                    {
                        Msg = "List Invoice input is null";
                    }
                }
            }
            return RedirectToPage();
        }
    }
}

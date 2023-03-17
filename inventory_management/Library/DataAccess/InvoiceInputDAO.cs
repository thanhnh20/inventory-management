using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess
{
    public class InvoiceInputDAO
    {
        InventoryManagementContext db = new InventoryManagementContext();

        private static InvoiceInputDAO instance = null;
        private static readonly object instancelock = new object();
        private InvoiceInputDAO() { }
        public static InvoiceInputDAO Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                    {
                        instance = new InvoiceInputDAO();
                    }
                    return instance;
                }
            }
        }

        public bool CreateInvoiceInput(InvoiceInput invoiceInput, Consignment consignment, List<Product> listProduct)
        {
            using (InventoryManagementContext dbContext = new InventoryManagementContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        Consignment newConsignment = new Consignment()
                        {
                            ConsignmentName = consignment.ConsignmentName,
                            Status = 1
                        };

                        dbContext.Consignments.Add(newConsignment);
                        dbContext.SaveChanges();


                        InvoiceInput newInvoiceInput = new InvoiceInput()
                        {
                            SuplierId = invoiceInput.SuplierId,
                            UserId = invoiceInput.UserId,
                            InputDate = invoiceInput.InputDate,
                            Amount = invoiceInput.Amount,
                        };
                        dbContext.InvoiceInputs.Add(newInvoiceInput);
                        dbContext.SaveChanges();

                        foreach (var product in listProduct)
                        {
                            ConsignmentDetail newConsignmentDetail = new ConsignmentDetail()
                            {
                                ConsignmentId = newConsignment.ConsignmentId,
                                ProductId = product.ProductId,
                                Quantity = product.TotalQuantity
                            };
                            dbContext.ConsignmentDetails.Add(newConsignmentDetail);
                            dbContext.SaveChanges();

                            var Product = dbContext.Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();
                            Product.TotalQuantity += product.TotalQuantity;
                            dbContext.SaveChanges();

                            InvoiceInputDetail invoiceInputDetail = new InvoiceInputDetail()
                            {
                                InputBillId = newInvoiceInput.InputBillId,
                                ConsignmentDetailId = newConsignmentDetail.ConsignmentDetailId,
                                Quantity = (int)product.TotalQuantity
                            };
                            dbContext.InvoiceInputDetails.Add(invoiceInputDetail);
                            dbContext.SaveChanges();
                        }                          

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                    return false;
                }
            }
        }

        public List<InvoiceInput> ListAll()
        {
            using (var db = new InventoryManagementContext())
            {
                return db.InvoiceInputs.Include(i => i.Suplier)
                                        .Include(i => i.InvoiceInputDetails)
                                        .OrderByDescending(i => i.InputDate)
                                        .ToList();
            }
        }
    }
}

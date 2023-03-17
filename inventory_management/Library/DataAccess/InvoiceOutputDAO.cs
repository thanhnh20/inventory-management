using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess
{
    public class InvoiceOutputDAO
    {
        private static InvoiceOutputDAO instance = null;
        private static readonly object instancelock = new object();
        private InvoiceOutputDAO() { }
        public static InvoiceOutputDAO Instance
        {
            get
            {
                lock (instancelock)
                {
                    if (instance == null)
                    {
                        instance = new InvoiceOutputDAO();
                    }
                    return instance;
                }
            }
        }
        
        public bool CreateInvoiceOutput(InvoiceOutput invoiceOutput, List<Product> listProduct)
        {
            using (InventoryManagementContext dbContext = new InventoryManagementContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var product in listProduct)
                        {
                            ConsignmentDetail newConsignmentDetail = new ConsignmentDetail()
                            {
                                ConsignmentId = (int)product.Status,
                                ProductId = product.ProductId,
                                Quantity = product.TotalQuantity
                            };
                            dbContext.ConsignmentDetails.Add(newConsignmentDetail);
                            dbContext.SaveChanges();

                            var Product = dbContext.Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();
                            Product.TotalQuantity -= product.TotalQuantity;
                            dbContext.SaveChanges();
                        }


                        InvoiceOutput newInvoiceOutput = new InvoiceOutput()
                        {
                            CustomerId = invoiceOutput.CustomerId,
                            UserId = invoiceOutput.UserId,
                            OutputDate = invoiceOutput.OutputDate,
                            Amount = invoiceOutput.Amount,
                        };
                        dbContext.InvoiceOutputs.Add(newInvoiceOutput);
                        dbContext.SaveChanges();

                        foreach (Product product in listProduct)
                        {
                            InvoiceOutputDetail invoiceOutputDetail = new InvoiceOutputDetail()
                            {
                                OutputBillId = newInvoiceOutput.OutputBillId,
                                ConsignmentId = (int)product.Status
                            };
                            dbContext.InvoiceOutputDetails.Add(invoiceOutputDetail);
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
    }
}

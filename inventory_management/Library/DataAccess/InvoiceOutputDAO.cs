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
                        InvoiceOutput newInvoiceOutput = new InvoiceOutput()
                        {
                            CustomerId = invoiceOutput.CustomerId,
                            UserId = invoiceOutput.UserId,
                            OutputDate = invoiceOutput.OutputDate,
                            Amount = invoiceOutput.Amount,
                        };
                        dbContext.InvoiceOutputs.Add(newInvoiceOutput);
                        dbContext.SaveChanges();

                        foreach (var productOutput in listProduct)
                        {
                            var consignmentID = productOutput.Status;
                            var consignmentDetail = dbContext.ConsignmentDetails.Where(l => l.ProductId == productOutput.ProductId && l.ConsignmentId == consignmentID).FirstOrDefault();
                            var product = dbContext.Products.Where(p => p.ProductId == productOutput.ProductId).FirstOrDefault();
                            consignmentDetail.Quantity -=  productOutput.TotalQuantity;
                            product.TotalQuantity -= productOutput.TotalQuantity;
                            
                            dbContext.SaveChanges();

                            if(product.TotalQuantity == 0)
                            {
                                product.Status = 0;
                            }
                            dbContext.SaveChanges();

                            InvoiceOutputDetail invoiceOutputDetail = new InvoiceOutputDetail()
                            {
                                OutputBillId = newInvoiceOutput.OutputBillId,
                                ConsignmentDetailId = consignmentDetail.ConsignmentDetailId,
                                Quantity = (int)productOutput.TotalQuantity
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

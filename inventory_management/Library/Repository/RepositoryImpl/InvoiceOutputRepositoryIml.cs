using Library.DataAccess;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.RepositoryImpl
{
    public class InvoiceOutputRepositoryIml : IInvoiceOutputRepository
    {
        public bool CreateInvoiceOutput(InvoiceOutput invoiceOutput, List<Product> listProduct) => InvoiceOutputDAO.Instance.CreateInvoiceOutput(invoiceOutput, listProduct);
    }
}

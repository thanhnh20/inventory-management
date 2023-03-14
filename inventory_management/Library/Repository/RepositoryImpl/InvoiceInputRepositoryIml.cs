using Library.DataAccess;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.RepositoryImpl
{
    public class InvoiceInputRepositoryIml : IInvoiceInputRepository
    {
        public bool CreateInvoiceInput(InvoiceInput invoiceInput, Consignment consignment, List<Product> listProduct) => InvoiceInputDAO.Instance.CreateInvoiceInput(invoiceInput, consignment, listProduct);
    }
}

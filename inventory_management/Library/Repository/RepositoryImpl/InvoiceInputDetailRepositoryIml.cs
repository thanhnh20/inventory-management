using Library.DataAccess;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.RepositoryImpl
{
    public class InvoiceInputDetailRepositoryIml : IInvoiceInputDetailRepository
    {
        public InvoiceInputDetail getConsignmentDetailIDByInputBill(int intputID) => InvoiceInputDetailDAO.Instance.getConsignmentDetailIDByInputBill(intputID);
    }
}

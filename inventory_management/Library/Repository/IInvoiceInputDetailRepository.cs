using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public interface IInvoiceInputDetailRepository
    {
        public InvoiceInputDetail getConsignmentDetailIDByInputBill(int intputID);

        public InvoiceInputDetail getInvoiceInputDetailById(int intputDetailID);
    }
}

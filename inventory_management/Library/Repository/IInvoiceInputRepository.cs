﻿using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public interface IInvoiceInputRepository
    {
        public bool CreateInvoiceInput(InvoiceInput invoiceInput, Consignment consignment, List<Product> listProduct);

        public List<InvoiceInput> ListAll();
    }
}

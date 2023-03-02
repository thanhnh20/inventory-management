using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Model
{
    public partial class Customer
    {
        public Customer()
        {
            InvoiceOutputs = new HashSet<InvoiceOutput>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public int Status { get; set; }

        public virtual ICollection<InvoiceOutput> InvoiceOutputs { get; set; }
    }
}

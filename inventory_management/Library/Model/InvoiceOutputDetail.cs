using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Model
{
    public partial class InvoiceOutputDetail
    {
        public int OutputDetailId { get; set; }
        public int OutputBillId { get; set; }
        public int ConsignmentId { get; set; }
        public int Quantity { get; set; }
        public double? TotalPrice { get; set; }

        public virtual Consignment Consignment { get; set; }
        public virtual InvoiceOutput OutputBill { get; set; }
    }
}

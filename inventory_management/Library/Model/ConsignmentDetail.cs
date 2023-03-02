using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Model
{
    public partial class ConsignmentDetail
    {
        public int ConsignmentDetailId { get; set; }
        public int ConsignmentId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Consignment Consignment { get; set; }
        public virtual Product Product { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class ProductConsignment
    {
        public int ProductConsignmentId { get; set; }
        public int ConsignmentId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Consignment Consignment { get; set; }
        public virtual Product Product { get; set; }
    }
}

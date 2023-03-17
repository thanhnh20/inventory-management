using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Model
{
    public partial class Product
    {
        public Product()
        {
            ConsignmentDetails = new HashSet<ConsignmentDetail>();
        }

        public int ProductId { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public string Unit { get; set; }
        public double? ImportPrice { get; set; }
        public double? SellingPrice { get; set; }
        public int? TotalQuantity { get; set; }
        public int? Status { get; set; }
        public string ProductName { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ConsignmentDetail> ConsignmentDetails { get; set; }
    }
}

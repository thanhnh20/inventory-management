using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Library.Model
{
    public partial class Consignment
    {
        public Consignment()
        {
            ConsignmentDetails = new HashSet<ConsignmentDetail>();
            InvoiceInputDetails = new HashSet<InvoiceInputDetail>();
            InvoiceOutputDetails = new HashSet<InvoiceOutputDetail>();
        }

        public int ConsignmentId { get; set; }
        [Required]
        public string ConsignmentName { get; set; }
        public int Status { get; set; }

        public virtual ICollection<ConsignmentDetail> ConsignmentDetails { get; set; }
        public virtual ICollection<InvoiceInputDetail> InvoiceInputDetails { get; set; }
        public virtual ICollection<InvoiceOutputDetail> InvoiceOutputDetails { get; set; }
    }
}

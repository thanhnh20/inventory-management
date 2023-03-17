using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Model
{
    public partial class Consignment
    {
        public Consignment()
        {
            ConsignmentDetails = new HashSet<ConsignmentDetail>();
        }

        public int ConsignmentId { get; set; }
        public string ConsignmentName { get; set; }
        public int Status { get; set; }

        public virtual ICollection<ConsignmentDetail> ConsignmentDetails { get; set; }
    }
}

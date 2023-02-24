using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public int Status { get; set; }
    }
}

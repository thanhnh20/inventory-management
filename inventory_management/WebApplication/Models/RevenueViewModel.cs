using System;

namespace WebApplication.Models
{
    public class RevenueViewModel
    {
        public int OutputBillId { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public DateTime OutputDate { get; set; }
        public double? Amount { get; set; }

        public double TotalPrice { get; set; }
    }
}

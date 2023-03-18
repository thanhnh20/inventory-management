using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class RevenueViewModel
    {
        public int OutputBillId { get; set; }

        [Display(Name = "Customer")]
        public string CustomerName { get; set; }
        [Display(Name = "Exporter Name")]
        public string ExporterName { get; set; }

        [Display(Name = "Export Date")]
        public DateTime OutputDate { get; set; }

        [Display(Name = "Export Amount")]
        public double? Amount { get; set; }

        [Display(Name = "Total Exported Price")]
        public double TotalPrice { get; set; }
    }
}

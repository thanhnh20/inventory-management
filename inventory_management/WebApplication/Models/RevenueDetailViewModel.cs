using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class RevenueDetailViewModel
    {
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }
        [Display(Name = "Consignment")]
        public string ConsignmentName { get; set; }
        [Display(Name = "Selling Price")]
        public double SellingPrice { get; set; }
        [Display(Name = "Unit")]
        public string Unit { get; set; }
        [Display(Name = "Quantity In Consignment")]
        public int QuantityLeft { get; set; }
        [Display(Name = "Sold Quantity")]
        public int QuantityExported { get; set; }
        [Display(Name = "Total Price Exported")]
        public double? TotalPrice { get; set; }


    }
}

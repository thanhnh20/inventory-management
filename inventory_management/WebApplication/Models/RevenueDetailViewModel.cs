namespace WebApplication.Models
{
    public class RevenueDetailViewModel
    {

        public string ProductName { get; set; }

        public string CategoryName { get; set; }

        public string Image { get; set; }
        public string ConsignmentName { get; set; }

        public double SellingPrice { get; set; }

        public string Unit { get; set; }
        public int Quantity { get; set; }
        public double? TotalPrice { get; set; }


    }
}

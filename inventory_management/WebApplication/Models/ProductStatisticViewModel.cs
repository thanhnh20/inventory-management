﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class ProductStatisticViewModel
    {
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Image")]
        public string Image { get; set; }
        [Display(Name = "Unit")]
        public string Unit { get; set; }
        [Display(Name = "Import Price")]
        public double ImportPrice { get; set; }
        [Display(Name = "Selling Price")]
        public double SellingPrice { get; set; }
        [Display(Name = "Consignment ID")]

        public int ConsignmentID { get; set; }
        [Display(Name = "Quantity")]

        public int Quantity { get; set; }

    }
}

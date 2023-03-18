using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

       /* [Required(ErrorMessage = "Product name is required")]*/
        public string ProductName { get; set; }

       /* [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]*/
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public string Image { get; set; }

       /* [Required(ErrorMessage = "Unit is required")]*/
        public string Unit { get; set; }

        public double? ImportPrice { get; set; }
        public double? SellingPrice { get; set; }
        [Display(Name = "Quantity")]
        public int? TotalQuantity { get; set; }
        public int? Status { get; set; }
      
      

        public virtual Category Category { get; set; }
        public virtual ICollection<ConsignmentDetail> ConsignmentDetails { get; set; }
        
        
        [Display(Name = "Front Image")]
        [NotMapped]
        public IFormFile FrontImage { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class SupplierViewModel
    {

        public int? SuplierId { get; set; }
        [Required]
        [Display(Name = "Supplier Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Supplier Name out of range length (> 0 <= 50)")]
        public string SuplierName { get; set; }
        [Required]
        [Display(Name = "Phone")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone must 10 digits")]
        public string SuplierPhone { get; set; }
        [Required]
        [Display(Name = "Tax Code")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Tax Code out of range length (> 0 <= 20)")]
        public string TaxCode { get; set; }
        public int Status { get; set; }
    }
}

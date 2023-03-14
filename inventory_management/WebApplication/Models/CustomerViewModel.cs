using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class CustomerViewModel
    {
        public int? CustomerId { get; set; }
        [Required]
        [Display(Name = "Customer Name")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Customer Name out of range length (> 0 <= 50)")]
        public string CustomerName { get; set; }
        [Required]
        [Display(Name = "Customer Address")]
        [EmailAddress]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Customer Address out of range length (> 0 <= 50)")]
        public string CustomerAddress { get; set; }
        [Required]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone must 10 digits")]
        public string CustomerPhone { get; set; }
        public int Status { get; set; }
    }
}

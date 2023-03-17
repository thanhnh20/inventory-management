using System;
using System.Collections.Generic;

#nullable disable

namespace AddDB.Model
{
    public partial class User
    {
        public User()
        {
            InvoiceInputs = new HashSet<InvoiceInput>();
            InvoiceOutputs = new HashSet<InvoiceOutput>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int? Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<InvoiceInput> InvoiceInputs { get; set; }
        public virtual ICollection<InvoiceOutput> InvoiceOutputs { get; set; }
    }
}

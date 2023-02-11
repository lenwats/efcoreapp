using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Models
{
    public class Customer : BaseModel
    {
        [DisplayName("Customer Name"), Required]
        public string CustomerName { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        [DisplayName("Street Address"), Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [DisplayName("Phone Number"), Required]
        public string Phone { get; set; }

        [Required]
        public string City { get; set; }

        public string Country { get; set; }
    }
}
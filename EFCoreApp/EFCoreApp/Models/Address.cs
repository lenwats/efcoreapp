using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Models
{
    public class Address : BaseModel
    {
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

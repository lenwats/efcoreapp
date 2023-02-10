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

        public virtual Address Address { get; set; }
    }
}
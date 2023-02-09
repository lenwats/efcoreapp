using System.Collections.Generic;

namespace EFCoreApp.Models
{
    public class Customer : BaseModel
    {
        public string CustomerName { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual Address Address { get; set; }
    }
}
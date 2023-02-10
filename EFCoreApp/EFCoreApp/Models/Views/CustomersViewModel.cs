using System.Collections;
using System.Collections.Generic;

namespace EFCoreApp.Models.Views
{
    public class CustomersViewModel : Customer
    {
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public int AddressId { get; set; }
    }
}

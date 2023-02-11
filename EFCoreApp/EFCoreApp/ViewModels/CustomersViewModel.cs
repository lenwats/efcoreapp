using System.Collections;
using System.Collections.Generic;
using EFCoreApp.Models;

namespace EFCoreApp.ViewModels
{
    public class CustomersViewModel : Customer
    {
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public int AddressId { get; set; }
    }
}

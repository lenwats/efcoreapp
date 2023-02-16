using EFCoreApp.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Collections;
using System.Collections.Generic;

namespace EFCoreApp.ViewModels
{
    public class AppointmentsViewModel 
    {
        public Appointment Appointment { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        public int SelectedCustomerId { get; set; }
    }
}

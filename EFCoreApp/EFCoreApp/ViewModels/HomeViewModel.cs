using System.Collections.Generic;
using EFCoreApp.Models;

namespace EFCoreApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}

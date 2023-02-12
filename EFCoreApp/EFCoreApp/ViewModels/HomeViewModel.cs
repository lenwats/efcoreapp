using System.Collections.Generic;
using EFCoreApp.Models;

namespace EFCoreApp.ViewModels
{
    public class HomeViewModel
    {
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}

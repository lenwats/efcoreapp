using System.Collections.Generic;

namespace EFCoreApp.Models.Views
{
    public class HomeViewModel
    {
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}

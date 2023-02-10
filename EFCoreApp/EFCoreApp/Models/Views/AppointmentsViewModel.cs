namespace EFCoreApp.Models.Views
{
    public class AppointmentsViewModel : Appointment
    {
        public int CustomerId { get; set; }

        public int AppUserId { get; set; }
    }
}

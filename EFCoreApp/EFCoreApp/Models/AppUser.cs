using System.Collections.Generic;

namespace EFCoreApp.Models
{
    public class AppUser : BaseModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
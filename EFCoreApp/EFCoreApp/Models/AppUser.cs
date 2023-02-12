using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Models
{
    public class AppUser : BaseModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
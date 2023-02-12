using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Models
{
    public class Appointment : BaseModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Contact { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        [DisplayName("Start Time"), Required]
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Notes { get; set; }

        public virtual AppUser AppUser { get; set; }

        public virtual Customer Customer { get; set; }
    }
}

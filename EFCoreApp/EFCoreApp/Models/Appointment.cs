using System;

namespace EFCoreApp.Models
{
    public class Appointment : BaseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Contact { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Notes { get; set; }

        public virtual AppUser AppUser { get; set; }

        public virtual Customer Customer { get; set; }
    }
}

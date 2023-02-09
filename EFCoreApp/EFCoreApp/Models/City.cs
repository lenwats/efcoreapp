using System.Collections.Generic;

namespace EFCoreApp.Models
{
    public class City : BaseModel
    {
        public string CityName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual Country Country { get; set; }
    }
}
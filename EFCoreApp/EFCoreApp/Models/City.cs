using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Models
{
    public class City : BaseModel
    {
        [DisplayName("City"), Required]
        public string CityName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual Country Country { get; set; }
    }
}
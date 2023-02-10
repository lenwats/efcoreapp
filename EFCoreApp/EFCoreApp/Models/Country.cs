using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Models
{
    public class Country : BaseModel
    {
        [DisplayName("Country"), Required]
        public string CountryName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
using System.Collections.Generic;

namespace EFCoreApp.Models
{
    public class Country : BaseModel
    {
        public string CountryName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
namespace EFCoreApp.Models
{
    public class Address : BaseModel
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }

        public virtual City City { get; set; }
    }
}

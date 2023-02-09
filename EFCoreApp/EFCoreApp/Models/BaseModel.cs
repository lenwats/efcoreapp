using System.ComponentModel.DataAnnotations;

namespace EFCoreApp.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}

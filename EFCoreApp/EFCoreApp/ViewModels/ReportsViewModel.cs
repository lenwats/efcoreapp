using EFCoreApp.Models;
using System.Collections.Generic;

namespace EFCoreApp.ViewModels
{
    public class ReportsViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }

        public Customer Customer { get; set; }

        public int SelectedCustomerId { get; set; }
    }
}

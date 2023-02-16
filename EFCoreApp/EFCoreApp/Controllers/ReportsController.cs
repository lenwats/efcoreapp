using AutoMapper;
using EFCoreApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using EFCoreApp.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Controllers
{
    public class ReportsController : BaseController
    {
        public ReportsController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [Authorize]
        public IActionResult Index()
        {
            var customers = Context.Customers.OrderBy(c => c.Id).ToList();

            var viewModel = new ReportsViewModel
            {
                Customers = customers,
            };

            PopulateCustomerList();

            return View(viewModel);
        }

        private void PopulateCustomerList(object selectedCustomer = null)
        {
            var customersQuery = Context.Customers.OrderBy(c => c.Id).ToList();

            ViewBag.CustomerList = new SelectList(customersQuery, "Id", "CustomerName", selectedCustomer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateReport(int? id, ReportsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    viewModel.Customer = await Context.Customers.FirstOrDefaultAsync(x => x.Id == viewModel.SelectedCustomerId);

                    //Context.Update(viewModel);
                    //await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(viewModel.Customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return View(viewModel);
        }

        private bool CustomerExists(int id)
        {
            return Context.Customers.Any(e => e.Id == id);
        }
    }
}

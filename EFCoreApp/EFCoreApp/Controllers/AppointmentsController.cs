using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFCoreApp.Data;
using EFCoreApp.Models;
using AutoMapper;
using EFCoreApp.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace EFCoreApp.Controllers
{
    public class AppointmentsController : BaseController
    {
        public AppointmentsController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var appts = from a in Context.Appointments select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                appts = appts.Where(a => a.Title.Contains(searchString)
                                    || a.Customer.CustomerName.Contains(searchString)
                                    || a.Contact.Contains(searchString)
                                    || a.Location.Contains(searchString)
                                    || a.Type.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    appts = appts.OrderByDescending(a => a.Title);
                    break;
                case "Date":
                    appts = appts.OrderBy(a => a.Start);
                    break;
                case "date_desc":
                    appts = appts.OrderByDescending(a => a.Start);
                    break;
                default:
                    appts = appts.OrderBy(a => a.Title);
                    break;
            }

            return View(await appts.Include(c => c.Customer).AsNoTracking().ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await Context.Appointments.Include(c => c.Customer).FirstOrDefaultAsync(x => x.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create(int? id)
        {

            var appointment = Context.Appointments.FirstOrDefault(x => x.Id == id);

            var viewModel = new AppointmentsViewModel
            {
                Appointment = appointment,
                Customers = Context.Customers
            };

            PopulateCustomerList();
            return View(viewModel);
            //return View();
        }

        // POST: Appointments/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Title,Description,Location,Contact,Type,Url,Start,End,Notes,Id")] AppointmentsViewModel appt)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Context.Add(appt);
        //        await Context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(appt);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, AppointmentsViewModel viewModel)
        {
            if (id != viewModel?.Appointment?.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    viewModel.Appointment.Customer = await Context.Customers.FirstOrDefaultAsync(x => x.Id == viewModel.SelectedCustomerId);

                    Context.Update(viewModel.Appointment);
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(viewModel.Appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await Context.Appointments.FirstOrDefaultAsync(x => x.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            var viewModel = new AppointmentsViewModel
            {
                Appointment = appointment,
                Customers = Context.Customers
            };

            return View(viewModel);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentsViewModel viewModel)
        {
            if (id != viewModel?.Appointment?.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    viewModel.Appointment.Customer = await Context.Customers.FirstOrDefaultAsync(x => x.Id == viewModel.SelectedCustomerId);

                    Context.Update(viewModel.Appointment);
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(viewModel.Appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await Context.Appointments.FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await Context.Appointments.FindAsync(id);
            Context.Appointments.Remove(appointment);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return Context.Appointments.Any(e => e.Id == id);
        }

        private void PopulateCustomerList(object selectedCustomer = null)
        {
            //var customersQuery = from c in Context.Customers orderby c.Id select c;
            var customersQuery = Context.Customers.OrderBy(c => c.Id).ToList();

            ViewBag.CustomerList = new SelectList(customersQuery, "Id", "CustomerName", selectedCustomer);

        }

    }
}

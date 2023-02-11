using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFCoreApp.Data;
using EFCoreApp.Models;
using EFCoreApp.Models.Views;
using AutoMapper;

namespace EFCoreApp.Controllers
{
    public class AppointmentsController : BaseController
    {
        public AppointmentsController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
           
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            return View(await Context.Appointments.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var appointment = await _context.Appointments.FirstOrDefaultAsync(m => m.Id == id);
            var appointment = await Context.Appointments.Include(c => c.Customer).FirstOrDefaultAsync(x => x.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            PopulateCustomerList();
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Location,Contact,Type,Url,Start,End,Notes,Id")] AppointmentsViewModel appointment)
        {
            if (ModelState.IsValid)
            {
                Context.Add(appointment);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await Context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
            //var appointment = await _context.Appointments
            //    .Include(c => c.Customer)
            //    .ThenInclude(cu => cu.Id)
            //    .Include(u => u.AppUser)
            //    .ThenInclude(ap => ap.Id)
            //    .FirstOrDefaultAsync(x => x.Id == id);

            PopulateCustomerList(appointment.Customer);

            if (appointment == null)
            {
                return NotFound();
            }

            var model = Mapper.Map<AppointmentsViewModel>(appointment);

            return View(model);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Customer,Description,Location,Contact,Type,Url,Start,End,Notes,Id")] AppointmentsViewModel appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }


            // CUSTOMER ID IS 0
            if (ModelState.IsValid)
            {
                try
                {
                    Context.Attach(appointment);
                    var customer = Context.Customers.FirstOrDefault(x => x.Id == appointment.CustomerId);
                    appointment.Customer = customer;

                    Context.Update(appointment);
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
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
            return View(appointment);
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
            var customersQuery = from c in Context.Customers orderby c.Id select c;

            ViewBag.CustomerList = new SelectList(customersQuery, "Id", "CustomerName", selectedCustomer);

        }

    }
}

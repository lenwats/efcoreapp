using AutoMapper;
using EFCoreApp.Data;
using EFCoreApp.Models;
using EFCoreApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            //_logger = logger;
        }

        public IActionResult Index()
        {
            var appts = Context.Appointments.Include(c => c.Customer).ToList();

            var model = new HomeViewModel
            {
                Appointments = appts
            };

            // TODO: Need to get logged in user

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

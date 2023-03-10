using AutoMapper;
using EFCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EFCoreApp.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMapper Mapper;
        protected readonly ApplicationDbContext Context;

        public BaseController(ApplicationDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
    }

}

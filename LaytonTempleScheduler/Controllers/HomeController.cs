using LaytonTempleScheduler.DataAccess;
using LaytonTempleScheduler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTempleScheduler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ISchedulerRepository _service { get; }

       

        public HomeController(ILogger<HomeController> logger, ISchedulerRepository temp)
        {
            _logger = logger;
            _service = temp;
        }

        public IActionResult Index(int mydateparamater)
        {
            return View();
        }

        public IActionResult SignUp()
        {

            ViewBag.startDate = _service.timeSlots
                .OrderBy(t => t.Start);
            //ViewBag.timeSlots = _service.timeSlots.Where(t => t.Start.Date == mydateparameter).OrderBy(t => t.Start);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

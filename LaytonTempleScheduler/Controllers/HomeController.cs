using LaytonTempleScheduler.DataAccess;
using LaytonTempleScheduler.Models;
using Microsoft.AspNetCore.Http;
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

        public IActionResult SignUp(string selectedDate = "")
        {
            DateTime selected;
            if (selectedDate == "" )
            {
                selected = DateTime.Today;
            }
            else
            {
                selected = DateTime.Parse(selectedDate);
            }
            List<TimeSlot> timeSlotsToDisplay = _service.timeSlots.Where(t => t.Start.Date == selected.Date).OrderBy(t => t.Start).ToList();

            ViewBag.selectedDate = selected.ToShortDateString();
            ViewBag.timeSlots = timeSlotsToDisplay;

            return View( );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Appointment(string startDate)
        {
            ViewBag.TimeSlot = startDate;

            return View();
        }

        [HttpPost]
        public IActionResult Appointment(Appointment a, string timeSlotStart)
        {
            TimeSlot temp = _service.timeSlots.Where(t => t.Start == DateTime.Parse(timeSlotStart)).First();
            _service.ReserveTimeSlot(temp);
            a.TimeSlot = temp;
            _service.AddAppointment(a);
            return View("Confirmation");
        }

        public IActionResult ViewAppointments()
        {
            List<Appointment> temp = _service.appointments.ToList();
            //foreach (Appointment app in temp)
            //{
            //    app.TimeSlot = _service.timeSlots.FirstOrDefault(t => t.Start == )
            //}
            ViewBag.appointments = temp;
            return View();
        }
        public IActionResult EditAppointment(IFormCollection form)
        {
            Appointment temp = _service.appointments.FirstOrDefault(a => a.AppointmentID == Convert.ToInt32(form["appID"]));
            temp.emailAddress = form["emailAddress"];
            temp.phoneNumber = form["phoneNumber"];
            temp.GroupSize = Convert.ToInt32(form["groupSize"]);
            temp.NameOfGroup = form["groupName"];

            _service.UpdateAppointment(temp);

            return RedirectToAction("ViewAppointments");
        }
    }
}

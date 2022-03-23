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

        //Prepares the SignUp.cshtml page. 
        //Parameter: selectedDate is the date the user wishes to see timeslots for, or the current date if nothing is selected
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

        //This is called when a time slot is selected, and a user goes to a form to reserve an appointment
        //Parameter: startDate is the datetime of the selected timeslot. 
        [HttpGet]
        public IActionResult Appointment(string startDate)
        {
            ViewBag.TimeSlot = startDate;

            return View();
        }

        //This post method is called when a new appointment is created.
        //Parameter: a is an appointment object
        //Parameter: timeslotstart is a string of the date time of the timeslot
        [HttpPost]
        public IActionResult Appointment(Appointment a, string timeSlotStart)
        {
            if (ModelState.IsValid) { 
            TimeSlot temp = _service.timeSlots.Where(t => t.Start == DateTime.Parse(timeSlotStart)).First();
            _service.ReserveTimeSlot(temp);
            //a.TimeSlot = temp;
            _service.AddAppointment(a);
            return View("Confirmation");
        }
            else
            {
                ViewBag.TimeSlot = timeSlotStart;
                return View();
            }
        }

        public IActionResult ViewAppointments()
        {
            List<Appointment> temp = _service.appointments.ToList();
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
        public IActionResult deleteApp(int AppointmentID)
        {
            Appointment temp = _service.appointments.FirstOrDefault(x => x.AppointmentID == AppointmentID);
            _service.RemoveAppointment(temp);
            return RedirectToAction("ViewAppointments");
        }
    }
}

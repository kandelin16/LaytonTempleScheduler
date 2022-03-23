using LaytonTempleScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTempleScheduler.DataAccess
{
    public class SchedulerRepository : ISchedulerRepository
    {
        private SchedulerContext _context { get; set; }
        public SchedulerRepository(SchedulerContext temp)
        {
            _context = temp;
        }

        public IQueryable<Appointment> appointments => _context.Appointments;
        public IQueryable<TimeSlot> timeSlots => _context.TimeSlots;

        public void InitializeTimeSlots()
        {
            _context.TimeSlots.RemoveRange(_context.TimeSlots);
        }
        public void AddAppointment(Appointment temp)
        {
            _context.Appointments.Add(temp);
            _context.SaveChanges();
        }
        public void ReserveTimeSlot(TimeSlot temp)
        {
            TimeSlot match = _context.TimeSlots.FirstOrDefault(t => t.Start == temp.Start);
            match.Available = false;
            _context.SaveChanges();
        }
        public void UpdateAppointment(Appointment app)
        {
            Appointment match = _context.Appointments.FirstOrDefault(a => a.AppointmentID == app.AppointmentID);
            match.GroupSize = app.GroupSize;
            match.NameOfGroup = app.NameOfGroup;
            match.phoneNumber = app.phoneNumber;
            match.emailAddress = app.emailAddress;
            _context.SaveChanges();
        }

        public void RemoveAppointment(Appointment app)
        {
            TimeSlot temp = _context.TimeSlots.FirstOrDefault(t => t.Start == app.TimeSlotStart);
            temp.Available = true;
            _context.Appointments.Remove(app);
            _context.SaveChanges();
        }
    }
}

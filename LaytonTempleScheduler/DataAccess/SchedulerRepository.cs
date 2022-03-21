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
    }
}

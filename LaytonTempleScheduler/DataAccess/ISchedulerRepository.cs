using LaytonTempleScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTempleScheduler.DataAccess
{
    public interface ISchedulerRepository
    {
        IQueryable<Appointment> appointments { get;  }
        IQueryable<TimeSlot> timeSlots { get;  }
        void InitializeTimeSlots();
        void AddAppointment(Appointment temp);
        void ReserveTimeSlot(TimeSlot temp);
        void UpdateAppointment(Appointment app);
    }
}

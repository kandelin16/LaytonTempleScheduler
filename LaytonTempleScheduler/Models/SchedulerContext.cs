using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTempleScheduler.Models
{
    public class SchedulerContext : DbContext
    {
        public SchedulerContext()
        {
        }

        public SchedulerContext(DbContextOptions<SchedulerContext> options)
            : base(options)
        {
        }

        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

    }
}

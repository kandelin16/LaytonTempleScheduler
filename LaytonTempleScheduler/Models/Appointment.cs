using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTempleScheduler.Models
{
    public class Appointment
    {
        [Key]
        [Required]
        public int AppointmentID { get; set; }
        public int TimeSlotID { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public string NameOfGroup { get; set; }
        public int GroupSize { get; set; }
        public string emailAddress { get; set; }
        public string phoneNumber { get; set; }
    }
}

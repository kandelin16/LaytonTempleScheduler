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
        public DateTime TimeSlotStart { get; set; }
        //public TimeSlot TimeSlot { get; set; }

        [Required(ErrorMessage = "Please enter a group name")]
        public string NameOfGroup { get; set; }

        [Required(ErrorMessage = "Please enter a group size")]
        public int GroupSize { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        public string emailAddress { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        public string phoneNumber { get; set; }
    }
}

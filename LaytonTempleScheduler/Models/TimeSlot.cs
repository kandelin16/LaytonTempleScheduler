using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTempleScheduler.Models
{
    public class TimeSlot
    {
        [Key]
        [Required]
        public int TimeSlotID { get; set; }
        public DateTime Start { get; set; }
        public bool Available { get; set; }
    }
}

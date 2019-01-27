using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebRole1.Models
{
    public class Webinar : Course
    {
        public string URL { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Released")]
        public DateTime DateReleased { get; set; }
    }
}
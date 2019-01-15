using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Webinar : Course
    {
        public string URL { get; set; }
        public DateTime DateReleased { get; set; }
    }
}
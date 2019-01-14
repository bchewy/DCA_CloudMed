using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Seminar : Courses
    {

        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public int Capacity { get; set; }
        public string Venue { get; set; }

    }
}
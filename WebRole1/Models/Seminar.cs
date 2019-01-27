using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebRole1.Models
{
    public class Seminar : Course
    {

        public int Duration { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public int Capacity { get; set; }
        public string Venue { get; set; }

    }
}
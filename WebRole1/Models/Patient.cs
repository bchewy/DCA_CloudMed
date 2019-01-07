using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Patient : Person
    {
        public int PatientID { get; set; }
        public string Address { get; set; }
        public string ICNo { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Doctor : Person
    {
        public int DoctorID { get; set; }
        public string Specialty { get; set; }
        public int FacultyID { get; set; }
    }
}
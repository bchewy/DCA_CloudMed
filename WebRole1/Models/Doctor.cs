using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Doctor : Person
    {
        
        public int DoctorID { get; set; }

        [ForeignKey("Consultation")]
        public int ConsultationID { get; set; }

        //[ForeignKey("Course")]
        //public int CourseID { get; set; }

        public string Specialty { get; set; }

        //public virtual ICollection<Consultation> Consultations { get; set; }
        public virtual Consultation Consultation { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
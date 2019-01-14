using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public abstract class Courses
    {
        public int CourseID { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double CourseFee { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
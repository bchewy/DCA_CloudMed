using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public abstract class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [Display(Name = "Course Fee")]
        public double CourseFee { get; set; }
        public string CourseImageURL { get; set; }

        public string CourseThumbnailImageURL { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }

        public virtual Doctor Doctor { get; set; }

    }
}
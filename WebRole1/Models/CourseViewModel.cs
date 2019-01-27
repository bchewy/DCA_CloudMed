using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class CourseViewModel
    {
        public enum CourTypes { Webinar, Seminar}

        [Key]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [Display(Name = "Course Fee")]
        public double CourseFee { get; set; }
        public string CourseImageURL { get; set; }
        public string CourseThumbnailImageURL { get; set; }
        public int DoctorID { get; set; }
        public CourTypes CourseType { get; set; }

        // Seminar
        public int Duration { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public int Capacity { get; set; }
        public string Venue { get; set; }

        // Webinar
        public string URL { get; set; }
        [Display(Name = "Date Released")]
        [DataType(DataType.Date)]
        public DateTime DateReleased { get; set; }
    }
}
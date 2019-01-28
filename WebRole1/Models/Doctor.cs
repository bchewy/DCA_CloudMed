using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Doctor : Person
    {
        
        public int DoctorID { get; set; }

        //[ForeignKey("Consultation")]
        //public int ConsultationID { get; set; }

        [DisplayName("Speciality")]
        [Required(ErrorMessage = "Key in the doctor's speciality!")]
        public string Specialty { get; set; }

        [DisplayName("Doctor image")]
        public string DoctorImageURL { get; set; }

        [DisplayName("Thumbnail")]
        public string DoctorThumbnailImageURL { get; set; }


        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Consultation> Consultations { get; set; }
    }
}
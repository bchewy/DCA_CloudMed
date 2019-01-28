using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Patient : Person
    {
        [Key]
        public int PatientID { get; set; }

        //[ForeignKey("Consultation")]
        //public int ConsultationID { get; set; }
        [DisplayName("Patient Image")]
        public string PatientImageURL { get; set; }

        [DisplayName("Thumbnail")]
        public string PatientThumbNailURl { get; set; }

        [DisplayName("Patient Address")]
        [Required(ErrorMessage = "Enter an address for this patient!")]
        public string Address { get; set; }

        [DisplayName("Thumbnail")]
        [Required(ErrorMessage = "Enter a date of birth for this patient!")]
        public DateTime DoB { get; set; }

        public virtual ICollection<Consultation> Consultations { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Patient : Person
    {
        public int PatientID { get; set; }
        [ForeignKey("Consultation")]
        public int ConsultationID { get; set; }

        public string Address { get; set; }
        public DateTime DoB { get; set; }

        public virtual ICollection<Consultation> Consultations { get; set; }
    }
}
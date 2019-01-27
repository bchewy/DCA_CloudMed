using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Consultation
    {
        [Key]
        public int ConsultationID { get; set; }

        public int QueueNo { get; set; }

        [Display(Name = "Consultation Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yy H:mm:ss tt}"), DataType(DataType.DateTime)]
        [Required]
        public DateTime TimeStamp { get; set; }

        [Required]
        public string Status { get; set; }
        [Required]
        [Display(Name = "Consultation Type")]
        public string ConsultationType { get; set; }

        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }
        
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        //public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
        public string JavascriptToRun { get; internal set; }
    }
}
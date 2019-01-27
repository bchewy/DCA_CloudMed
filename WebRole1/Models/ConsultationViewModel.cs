using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class ConsultationViewModel
    {
        [Key]
        public int ConsultationID { get; set; }

        public int QueueNo { get; set; }

        [Display(Name = "Consultation Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yy H:mm:ss tt}"), DataType(DataType.DateTime)]
        [Required]
        public DateTime TimeStamp { get; set; }

        public string PatientName { get; set; }

        public string DoctorName { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string ConsultationType { get; set; }
    }
}
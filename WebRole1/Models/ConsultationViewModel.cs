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
        [Display(Name = "Consultation Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yy H:mm:ss tt}"), DataType(DataType.DateTime)]
        [Required]
        public DateTime TimeStamp { get; set; }
        [Required]
        public int QueueNo { get; set; }

        public string QueueColor { get; set; }
        public string DateColor { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string ConsultationType { get; set; }

        public string typeColor { get; set; }
        [Required]
        public Doctor doctor { get; set; }

        [Required]
        public Patient patient { get; set; }
    }
}
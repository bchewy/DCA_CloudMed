using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebRole1.Models
{
    public class MedicalEquipmentViewModel
    {
        [Key]
        public int EquipmentID { get; set; }

        [Required(ErrorMessage = "You must enter the name of the medical equipment")]
        [Display(Name ="Equipment Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must enter the brand of the medical equipment")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "You must enter the serial number of the medical equipment")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "What is the status of the medical equiment now?")]
        public string Status { get; set; }

        [Required(ErrorMessage = "What is the serial number of this medical equipment")]
        public string SoftwareVersion { get; set; }

        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }

    }

}
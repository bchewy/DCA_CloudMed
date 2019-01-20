using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class PatientViewModel
    {
        [Key]
        public int PatientID { get; set; }

        [Display (Name = "IC Number")]
        [Required (ErrorMessage ="IC Number is required.")]
        public string ICNo { get; set; }

        [Required (ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DoB { get; set; }


        [Required(ErrorMessage = "Gender is required.")]
        public char Gender { get; set; }

        [Required(ErrorMessage = "Citizenship is required.")]
        public string Citizenship { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress]
        public string EmailAddr { get; set; }
    }
}
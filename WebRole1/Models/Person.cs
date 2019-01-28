using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public abstract class Person
    {
        public int PersonID { get; set; }


        [DisplayName("Person's Identitifcation Number")]
        [Required(ErrorMessage = "Key in the IC!")]
        public string ICNo { get; set; }

        [DisplayName("Peron's full name")]
        [Required(ErrorMessage = "Key in their name!")]
        public string Name { get; set; }

        [DisplayName("Person's gender")]
        [Required(ErrorMessage = "Key in a gender (F/M only)")]
        public char Gender { get; set; }

        [DisplayName("Person's citizenship")]
        [Required(ErrorMessage = "Key in citizenship!")]
        public string Citizenship { get; set; }

        [DisplayName("Person's Email Address")]
        [Required(ErrorMessage = "Key in the emailaddress!")]
        public string EmailAddr { get; set; }
    }
}
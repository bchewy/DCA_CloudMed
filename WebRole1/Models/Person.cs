using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public abstract class Person
    {
        public int PersonID { get; set; }
        public string ICNo { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        public string Citizenship { get; set; }
        public string EmailAddr { get; set; }
    }
}
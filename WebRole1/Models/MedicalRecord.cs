using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class MedicalRecord
    {
        [Key]
        public int RecordID { get; set; }



        public string Description { get; set; }
        public string DocURL { get; set; }

        [ForeignKey("Diagnosis")]
        public int DiagnosisID { get; set; }

        public virtual Diagnosis Diagnosis { get; set; }
    }
}
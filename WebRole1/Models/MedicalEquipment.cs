using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class MedicalEquipment
    {
        [Key]
        public int EquipmentID { get; set; }
        [ForeignKey("Diagnosis")]
        public int DiagnosisID { get; set; }

        public string Name { get; set; }
        public string Brand { get; set; }
        public string SerialNumber { get; set; }
        public string Status { get; set; }
        public string SoftwareVersion { get; set; }
        public int Warranty { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime LastMaintenance { get; set; }

        public virtual Diagnosis Diagnosis { get; set; }
    }
}
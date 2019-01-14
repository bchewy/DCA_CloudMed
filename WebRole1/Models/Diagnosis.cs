using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class Diagnosis
    {
        public int DiagnosisID { get; set; }
        [ForeignKey("Consultation")]
        public int ConsultationID { get; set; }
        [ForeignKey("MedicalEquipment")]
        public int MedicalEquipmentID { get; set; }
        [ForeignKey("MedicalRecord")]
        public int RecordID { get; set; }

        public string Illness { get; set; }
        public string Description { get; set; }

        public virtual Consultation Consultation { get; set; }
        public virtual ICollection<MedicalEquipment> MedicalEquipments { get; set; }
        public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
    }
}
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
        [Required]
        public string Description { get; set; }
        public string DocURL { get; set; }

        public string Illness { get; set; }

        [ForeignKey("Consultation")]
        public int ConsultationID { get; set; }

        public virtual Consultation Consultation { get; set; }

        public virtual ICollection<MedicalEquipment> MedicalEquipments { get; set; }
    }
}
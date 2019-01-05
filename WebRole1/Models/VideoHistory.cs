using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class VideoHistory
    {

        public int VideoID { get; set; }

        [ForeignKey("Person")]
        public int PersonID { get; set; }

        public DateTime Duration { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual Person person { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace W8_Backend.Data.Entities
{
    public class Logs
    {
        public int LogID { get; set; }
        public string? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LogDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LogEndDate { get; set; }

        public virtual ICollection<LogDetails>? LogDetails { get; set; }
    }
}







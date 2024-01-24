
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace W8_Backend.Data.Entities
{
    public class LogDetails
    {
    public int DetailID { get; set; }
    public int LogID { get; set; }
    public string? Description { get; set; }
    public string? AdditionalInfo { get; set; }
    public string? Status { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime LogDetailTimeStamp { get; set; }
    public virtual Logs? Log { get; set; }
}
}







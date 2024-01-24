using W8_Backend.Data.Entities;

namespace W8_Backend.Models.LogModels.Output
{
    public class LogView
    {
        public Logs Log { get; set; }
        public bool HasDetails { get; set; }
    }
}

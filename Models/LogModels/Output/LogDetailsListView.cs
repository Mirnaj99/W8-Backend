using W8_Backend.Data.Entities;
using System.Collections.Generic;

namespace W8_Backend.Models.LogModels.Output
{
    public class LogDetailsListView
    {
        public List<LogDetails> LogDetails { get; set; }
        public long TotalNumberOfRecords { get; set; }
    }
}

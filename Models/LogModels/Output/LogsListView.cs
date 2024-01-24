using System.Collections.Generic;

namespace W8_Backend.Models.LogModels.Output
{
    public class LogsListView
    {
      public  List<LogView> Logs { get; set; }
        public long TotalNumberOfRecords { get; set; }

    }
}

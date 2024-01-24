using W8_Backend.Models.CommonModels.Input;

namespace W8_Backend.Models.LogModels.Input
{
    public class GetLogDetailsRequest
    {
        public int LogID { get; set; }
        public PaginationModel Keys { get; set; }
        public LogDetailsFilter Filters { get; set; }
        public string OrderColumn { get; set; }
        public string OrderDirection { get; set; }
    }
}

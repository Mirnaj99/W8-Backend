using W8_Backend.Models.CommonModels.Input;

namespace W8_Backend.Models.LogModels.Input
{
    public class GetLogsListRequest
    {
        public LogFilters Filters { get; set; }
        public PaginationModel Keys { get; set; }
        public string OrderColumn { get; set; }
        public string OrderDirection { get; set; }
    }
}

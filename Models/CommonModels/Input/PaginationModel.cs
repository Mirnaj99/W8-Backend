

using W8_Backend.Models.UserModels.Input;

namespace W8_Backend.Models.CommonModels.Input
{
    public class PaginationModel
    {
        public int Page { get; set; }
        public int PageSize { get; set; }   
        public string KeyWord { get; set; }
    }
}



using W8_Backend.Models.CommonModels.Input;

namespace W8_Backend.Models.UserModels.Input
{
    public class GetUsersListModel
    {
        public PaginationModel Keys  { get; set; }
        public bool ViewAllUsers { get; set; }
        public int ExcludedCompanyID { get; set; }
        public int? CompanyID { get; set; }
        public int? ExcludedUserID { get; set; }
        public string OrderColumn { get; set; }
        public string OrderDirection { get; set; }

    }
}

using W8_Backend.Models.LogModels.Input;
using W8_Backend.Models.LogModels.Output;
using System.Threading.Tasks;

namespace W8_Backend.Services.Interfaces
{
    public interface ILogsService
    {
        //Defining services of Logs service
        Task<LogsListView> GetLogsListAsync(GetLogsListRequest model);
        Task<LogDetailsListView> GetLogDetailsListAsync(GetLogDetailsRequest model);
        Task DeleteLogsListAsync(DeleteLogsRequest model);
        Task DeleteLogDetailsListAsync(DeleteLogDetailsRequest model);

    }
}

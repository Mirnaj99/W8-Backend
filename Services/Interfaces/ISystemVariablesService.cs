using W8_Backend.Data.Entities;
using W8_Backend.Models.FileModels;
using W8_Backend.Models.SystemVariables;

namespace W8_Backend.Services.Interfaces
{
    public interface ISystemVariablesService
    {
        Task UpdateMiddlewareFieldsAsync(UpdateMiddlewareFields model);

        Task UpdateHRFieldsAsync(UpdateMiddlewareFields model);
        Task UpdateSyncDateFieldAsync(UpdateMiddlewareFields model);

        Task UpdateDeleteRecordsFieldsAsync(UpdateMiddlewareFields model);
        Task UpdateSyncEmpDiffFieldsAsync();
        Task<SystemVariables> GetSystemVariablesDataAsync();
        Task UpdateMiddlewareSyncMonthlyCostSheetFieldAsync();

        //Task UploadSheetAndSync(FileModel model);
    }
}
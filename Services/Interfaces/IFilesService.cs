
using W8_Backend.Models.FileModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace W8_Backend.Services.Interfaces
{
    public interface IFilesService

    {
        Task<string> UploadFileAsync(FileModel file);
    }
}

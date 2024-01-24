using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using W8_Backend.Data;
using W8_Backend.Helpers;
using W8_Backend.Models.FileModels;
using W8_Backend.Services.Interfaces;

namespace W8_Backend.Services
{



    public class FilesService : IFilesService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly string  fileUploadPath;
        private readonly string fileFetchPath;
        public FilesService(DataContext context)
        {
            _context = context;
            //Initializing the required variables, and defining the required services using dependency injection      
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile("Utils\\UtilizedText\\Errors\\English_Errors.json", optional: true)
           .AddEnvironmentVariables();
            _configuration = builder.Build();
            var systemVariables = _context.SystemVariables.FirstOrDefault();
            fileUploadPath = systemVariables.MonthlyCostSheetPath;
            //fileFetchPath = _configuration.GetSection("App-Utils")["FileFetchPath"];
            
        }
        public async Task<string> UploadFileAsync(FileModel file)
        {
            try
            {
                if (file.File.Length > 0)
                {
                    if (!Directory.Exists(fileUploadPath))
                    {
                        Directory.CreateDirectory(fileUploadPath);
                    };

                    string ext = Path.GetExtension(file.File.FileName);
                    string fileName = file.File.FileName;
                    using FileStream fileStream = File.Create(fileUploadPath + fileName);
                    file.File.CopyTo(fileStream);
                    fileStream.Flush();
                    return fileUploadPath + fileName;
                }
                else
                {
                    throw new AppException("err350", _configuration);

                }
            }
            catch (AppException ex)
            {
                throw new AppException(ex.Message, _configuration);
            }
            catch (Exception ex)
            {
                throw new AppException("err000", ex.InnerException != null ? ex.InnerException.Message : ex.Message, _configuration);
            }
        }
    }
}

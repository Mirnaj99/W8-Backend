using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using W8_Backend.Data;
using W8_Backend.Data.Entities;
using W8_Backend.Helpers;
using W8_Backend.Models.FileModels;
using W8_Backend.Models.SystemVariables;
using W8_Backend.Services.Interfaces;
using W8_Backend.Services;

namespace W8_Backend.Services
{
    public class SystemVariablesService : ISystemVariablesService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;






        public SystemVariablesService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            //Initializing the required variables, and defining the required services using dependency injection
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile("Utils\\UtilizedText\\Errors\\English_Errors.json", optional: true)
           .AddEnvironmentVariables();

        }

        //public async Task UploadSheetAndSync(FileModel file)
        //{
        //    try
        //    {
        //        _filesService.UploadFileAsync(file);
        //        UpdateMiddlewareSyncMonthlyCostSheetFieldAsync();
        //    } catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        public async Task UpdateMiddlewareSyncMonthlyCostSheetFieldAsync()
        {


            try
            {
                var recordToUpdate = await _context.SystemVariables.FirstOrDefaultAsync();
                if (recordToUpdate == null)
                    throw new AppException("err149", _configuration);

                recordToUpdate.SyncMonthlyCostSheet = true;


                _context.SystemVariables.Update(recordToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (AppException ex)
            {
                throw new AppException(ex.Message, _configuration);
            }
            catch (DbUpdateException ex)
            {

                SqlException err = (SqlException)ex.InnerException;
                var number = err == null ? 0 : err.Number;
                string s = err == null ? "" : err.Message;
                string secondMsg = "";

                string error = "";
                switch (number)
                {
                    case 2601:
                        // Unique Index/Constraint Violation
                        secondMsg = s;
                        break;
                    case 547:
                        // ForeignKey Violation on insert
                        secondMsg = s;
                        break;
                    default:
                        // throw a general db Exception
                        secondMsg = s;
                        break;
                }
                throw new AppException(error, _configuration);

            }
            catch (Exception ex)
            {
                throw new AppException("err000", ex.InnerException != null ? ex.InnerException.Message : ex.Message, _configuration);
            }
        }
        public async Task UpdateHRFieldsAsync(UpdateMiddlewareFields model)
        {


            try
            {
                var recordToUpdate = await _context.SystemVariables.FirstOrDefaultAsync();
                if (recordToUpdate == null)
                    throw new AppException("err149", _configuration);

                recordToUpdate.HRCompanyCode = model.HRCompanyCode;
                recordToUpdate.HRJobNb = model.HRJobNb;
                recordToUpdate.HRTaskNb = model.HRTaskNb;

                _context.SystemVariables.Update(recordToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (AppException ex)
            {
                throw new AppException(ex.Message, _configuration);
            }
            catch (DbUpdateException ex)
            {

                SqlException err = (SqlException)ex.InnerException;
                var number = err == null ? 0 : err.Number;
                string s = err == null ? "" : err.Message;
                string secondMsg = "";

                string error = "";
                switch (number)
                {
                    case 2601:
                        // Unique Index/Constraint Violation
                        secondMsg = s;
                        break;
                    case 547:
                        // ForeignKey Violation on insert
                        secondMsg = s;
                        break;
                    default:
                        // throw a general db Exception
                        secondMsg = s;
                        break;
                }
                throw new AppException(error, _configuration);

            }
            catch (Exception ex)
            {
                throw new AppException("err000", ex.InnerException != null ? ex.InnerException.Message : ex.Message, _configuration);
            }
        }

        public async Task UpdateSyncDateFieldAsync(UpdateMiddlewareFields model)
        {


            try
            {
                var recordToUpdate = await _context.SystemVariables.FirstOrDefaultAsync();
                if (recordToUpdate == null)
                    throw new AppException("err149", _configuration);
                if(model.LastSyncDate != null)
                recordToUpdate.LastSyncDate = model.LastSyncDate;

                _context.SystemVariables.Update(recordToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (AppException ex)
            {
                throw new AppException(ex.Message, _configuration);
            }
            catch (DbUpdateException ex)
            {

                SqlException err = (SqlException)ex.InnerException;
                var number = err == null ? 0 : err.Number;
                string s = err == null ? "" : err.Message;
                string secondMsg = "";

                string error = "";
                switch (number)
                {
                    case 2601:
                        // Unique Index/Constraint Violation
                        secondMsg = s;
                        break;
                    case 547:
                        // ForeignKey Violation on insert
                        secondMsg = s;
                        break;
                    default:
                        // throw a general db Exception
                        secondMsg = s;
                        break;
                }
                throw new AppException(error, _configuration);

            }
            catch (Exception ex)
            {
                throw new AppException("err000", ex.InnerException != null ? ex.InnerException.Message : ex.Message, _configuration);
            }
        }



        public async Task UpdateDeleteRecordsFieldsAsync(UpdateMiddlewareFields model)
        {


            try
            {
                var recordToUpdate = await _context.SystemVariables.FirstOrDefaultAsync();
                if (recordToUpdate == null)
                    throw new AppException("err149", _configuration);

                recordToUpdate.DeleteRecords = true;
                if(model.DocumentNumberToDelete != null)
                recordToUpdate.DocumentNumberToDelete = model.DocumentNumberToDelete;

                _context.SystemVariables.Update(recordToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (AppException ex)
            {
                throw new AppException(ex.Message, _configuration);
            }
            catch (DbUpdateException ex)
            {

                SqlException err = (SqlException)ex.InnerException;
                var number = err == null ? 0 : err.Number;
                string s = err == null ? "" : err.Message;
                string secondMsg = "";

                string error = "";
                switch (number)
                {
                    case 2601:
                        // Unique Index/Constraint Violation
                        secondMsg = s;
                        break;
                    case 547:
                        // ForeignKey Violation on insert
                        secondMsg = s;
                        break;
                    default:
                        // throw a general db Exception
                        secondMsg = s;
                        break;
                }
                throw new AppException(error, _configuration);

            }
            catch (Exception ex)
            {
                throw new AppException("err000", ex.InnerException != null ? ex.InnerException.Message : ex.Message, _configuration);
            }
        }

        public async Task UpdateMiddlewareFieldsAsync(UpdateMiddlewareFields model)
        {


            try
            {
                var recordToUpdate = await _context.SystemVariables.FirstOrDefaultAsync();
                if (recordToUpdate == null)
                    throw new AppException("err149", _configuration);

                recordToUpdate.LogCleanRuntime = model.LogCleanRuntime;
                recordToUpdate.Retention = (int)model.Retention;
                recordToUpdate.SyncStatus = model.MiddlewareStatus;

                _context.SystemVariables.Update(recordToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (AppException ex)
            {
                throw new AppException(ex.Message, _configuration);
            }
            catch (DbUpdateException ex)
            {

                SqlException err = (SqlException)ex.InnerException;
                var number = err == null ? 0 : err.Number;
                string s = err == null ? "" : err.Message;
                string secondMsg = "";

                string error = "";
                switch (number)
                {
                    case 2601:
                        // Unique Index/Constraint Violation
                        secondMsg = s;
                        break;
                    case 547:
                        // ForeignKey Violation on insert
                        secondMsg = s;
                        break;
                    default:
                        // throw a general db Exception
                        secondMsg = s;
                        break;
                }
                throw new AppException(error, _configuration);

            }
            catch (Exception ex)
            {
                throw new AppException("err000", ex.InnerException != null ? ex.InnerException.Message : ex.Message, _configuration);
            }
        }


        public async Task UpdateSyncEmpDiffFieldsAsync()
        {


            try
            {
                var recordToUpdate = await _context.SystemVariables.FirstOrDefaultAsync();
                if (recordToUpdate == null)
                    throw new AppException("err149", _configuration);
                DateTime syncDate = (DateTime)recordToUpdate.LastSyncDate;
                int month;
                int year;
                if (syncDate.Month < 12)
                {
                    month = syncDate.Month + 1;
                    year = syncDate.Year;
                }
                else
                {
                    month = 1;
                    year = syncDate.Year + 1;
                }

                int daysInMonth = DateTime.DaysInMonth(year, month);
                recordToUpdate.SyncEmpDiff = true;
                recordToUpdate.LastSyncDate = syncDate.AddDays(daysInMonth);

                _context.SystemVariables.Update(recordToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (AppException ex)
            {
                throw new AppException(ex.Message, _configuration);
            }
            catch (DbUpdateException ex)
            {

                SqlException err = (SqlException)ex.InnerException;
                var number = err == null ? 0 : err.Number;
                string s = err == null ? "" : err.Message;
                string secondMsg = "";

                string error = "";
                switch (number)
                {
                    case 2601:
                        // Unique Index/Constraint Violation
                        secondMsg = s;
                        break;
                    case 547:
                        // ForeignKey Violation on insert
                        secondMsg = s;
                        break;
                    default:
                        // throw a general db Exception
                        secondMsg = s;
                        break;
                }
                throw new AppException(error, _configuration);

            }
            catch (Exception ex)
            {
                throw new AppException("err000", ex.InnerException != null ? ex.InnerException.Message : ex.Message, _configuration);
            }
        }

        public async Task<SystemVariables> GetSystemVariablesDataAsync()
        {
            try
            {
                //Gets the system variables data to be returned
                var result = await _context.SystemVariables.FirstOrDefaultAsync();
                if (result == null)
                    throw new AppException("err149", _configuration);

                return result;

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

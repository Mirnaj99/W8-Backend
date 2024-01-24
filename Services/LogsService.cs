using System;
using W8_Backend.Data;
using W8_Backend.Data.Entities;
using W8_Backend.Helpers;
using W8_Backend.Models.LogModels.Input;
using W8_Backend.Models.LogModels.Output;
using W8_Backend.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace W8_Backend.Services
{
    public class LogsService : ILogsService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public LogsService(DataContext context)
        {
            //Initializing the required variables, and defining the required services using dependency injection
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile("Utils\\UtilizedText\\Errors\\English_Errors.json", optional: true)
           .AddEnvironmentVariables();
            _configuration = builder.Build();
            _context = context;
        }
        //Function to get list of logs
        public async Task<LogsListView> GetLogsListAsync(GetLogsListRequest model)
        {
            try
            {

                //Checking if the keys are empty (Keys model is responsible for the pagination)
                if (model.Keys == null)
                {
                    throw new AppException("err35", _configuration);
                }

                //Getting the skip value to get the list
                int skip = (model.Keys.Page - 1) * model.Keys.PageSize;


                LogsListView toBeReturned = new LogsListView();



                List<LogView> result = new List<LogView>();
                long count = 0;

                var baseQuery = _context.Logs.AsNoTracking().AsQueryable();




                //Applying the  filters on the Order table
                if (model.Filters != null)
                {
                    if (model.Filters.LogDateFrom != null)
                    {
               
                        baseQuery = baseQuery.Where(x => model.Filters.LogDateFrom.Value.Date <= x.LogDate.Date);
                    }
                    if (model.Filters.LogDateTo != null)
                    {
                        baseQuery = baseQuery.Where(x => model.Filters.LogDateTo.Value.Date >= x.LogDate.Date);
                    }
                }


                //For Sorting
                ////
                if (model.OrderColumn != "")
                {

                    if (model.OrderDirection == "asc")
                    {
                        baseQuery = baseQuery.OrderBy(x => EF.Property<Logs>(x, model.OrderColumn)).ThenBy(x => EF.Property<Logs>(x, model.OrderColumn).Equals(null));
                    }
                    else if (model.OrderDirection == "desc")
                    {
                        baseQuery = baseQuery.OrderByDescending(x => EF.Property<Logs>(x, model.OrderColumn)).ThenBy(x => EF.Property<Logs>(x, model.OrderColumn).Equals(null));
                    }
                }
                ////

                //Getting the list of Log records
                result = await (from log in baseQuery
                                select new LogView
                                {
                                    Log = log,
                                    HasDetails = _context.LogDetails.Count(x => x.LogID == log.LogID) > 0 ? true : false
                                }).Skip(skip)
                             .Take(model.Keys.PageSize)
                             .ToListAsync();
                //Getting the count of the result
                count = await baseQuery.CountAsync();

                //Filling the model to be returned with the result list and its count and returning this model
                toBeReturned.Logs = result;
                toBeReturned.TotalNumberOfRecords = count;
                return toBeReturned;
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("err000", ex.InnerException != null ? ex.InnerException.Message : ex.Message, _configuration);
            }
        }
        //Function to get list of log details for a certain log
        public async Task<LogDetailsListView> GetLogDetailsListAsync(GetLogDetailsRequest model)
        {
            try
            {


                //Checking if the keys are empty (Keys model is responsible for the pagination)
                if (model.Keys == null)
                {
                    throw new AppException("err35", _configuration);
                }

                //Getting the skip value to get the list
                int skip = (model.Keys.Page - 1) * model.Keys.PageSize;


                LogDetailsListView toBeReturned = new LogDetailsListView();



                List<LogDetails> result = new List<LogDetails>();
                long count = 0;

                var baseQuery = _context.LogDetails.AsNoTracking().AsQueryable();

                //For Sorting
                ////
                if (model.OrderColumn != "")
                {

                    if (model.OrderDirection == "asc")
                    {
                        baseQuery = baseQuery.OrderBy(x => EF.Property<LogDetails>(x, model.OrderColumn)).ThenBy(x => EF.Property<LogDetails>(x, model.OrderColumn).Equals(null));
                    }
                    else if (model.OrderDirection == "desc")
                    {
                        baseQuery = baseQuery.OrderByDescending(x => EF.Property<LogDetails>(x, model.OrderColumn)).ThenBy(x => EF.Property<LogDetails>(x, model.OrderColumn).Equals(null));
                    }
                }
                ////
                ///

                //For Filters
                if (model.Filters != null && !string.IsNullOrEmpty(model.Filters.Status))
                {
                    baseQuery = baseQuery.Where(x => x.Status.Contains(model.Filters.Status));
                }

                //Getting the list of Log details records
                result = await (from detail in baseQuery
                                where detail.LogID == model.LogID
                                select detail).Skip(skip)
                             .Take(model.Keys.PageSize)
                             .ToListAsync();
                //Getting the count of the result
                count = await baseQuery.Where(x => x.LogID == model.LogID).CountAsync();

                //Filling the model to be returned with the result list and its count and returning this model
                toBeReturned.LogDetails = result;
                toBeReturned.TotalNumberOfRecords = count;
                return toBeReturned;
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new AppException("err000", ex.InnerException != null ? ex.InnerException.Message : ex.Message, _configuration);
            }
        }

        public async Task DeleteLogsListAsync(DeleteLogsRequest model)
        {
            try
            {
                //Checking if list of Log records to be deleted is empty
                if (model == null)
                    throw new AppException("err334", _configuration);
                if (model.Logs == null)
                    throw new AppException("err334", _configuration);

                foreach (int id in model.Logs)
                {
                    //Checking if each Log record to be deleted exists in the database
                    var recordToBeDeleted = await _context.Logs.FindAsync(id);
                    if (recordToBeDeleted == null)
                    {
                        throw new AppException("err335", "" + id, _configuration);
                    }
                    //Removing the Log record
                    _context.Logs.Remove(recordToBeDeleted);
                    await _context.SaveChangesAsync();
                }
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (DbUpdateException ex)
            {

                SqlException err = (SqlException)ex.InnerException;
                var number = err == null ? 0 : err.Number;
                string s = err == null ? "" : err.Message;
                string scndMsg = "";

                string error = "";
                switch (number)
                {

                    case 547:
                        // ForeignKey Violation 

                        error = "err336";
                        scndMsg = err == null ? "" : err.Message;



                        break;
                    default:
                        // throw a general db Exception
                        error = "err336";
                        scndMsg = err == null ? "" : err.Message;
                        break;
                }
                throw new AppException(error, scndMsg, _configuration);

            }
            catch (Exception ex)
            {
                throw new AppException("err000", ex.InnerException != null ? ex.InnerException.Message : ex.Message, _configuration);
            }
        }

        public async Task DeleteLogDetailsListAsync(DeleteLogDetailsRequest model)
        {
            try
            {
                //Checking if list of Log details records to be deleted is empty
                if (model == null)
                    throw new AppException("err337", _configuration);
                if (model.LogDetails == null)
                    throw new AppException("err337", _configuration);

                foreach (int id in model.LogDetails)
                {
                    //Checking if each Log detail record to be deleted exists in the database
                    var recordToBeDeleted = await _context.LogDetails.FindAsync(id);
                    if (recordToBeDeleted == null)
                    {
                        throw new AppException("err338", "" + id, _configuration);
                    }
                    //Removing the Log detail record
                    _context.LogDetails.Remove(recordToBeDeleted);
                    await _context.SaveChangesAsync();
                }
            }
            catch (AppException ex)
            {
                throw ex;
            }
            catch (DbUpdateException ex)
            {

                SqlException err = (SqlException)ex.InnerException;
                var number = err == null ? 0 : err.Number;
                string s = err == null ? "" : err.Message;
                string scndMsg = "";

                string error = "";
                switch (number)
                {

                    case 547:
                        // ForeignKey Violation 

                        error = "err339";
                        scndMsg = err == null ? "" : err.Message;



                        break;
                    default:
                        // throw a general db Exception
                        error = "err339";
                        scndMsg = err == null ? "" : err.Message;
                        break;
                }
                throw new AppException(error, scndMsg, _configuration);

            }
            catch (Exception ex)
            {
                throw new AppException("err000", ex.InnerException != null ? ex.InnerException.Message : ex.Message, _configuration);
            }
        }
    }
}

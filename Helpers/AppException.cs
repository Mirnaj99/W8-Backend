
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using W8_Backend.Models.ErrorModels.Output;

namespace W8_Backend.Helpers
{
    //Custom exceptions class that returns an error with the error code provided (error messages are taken from English_Errors json file in Utils/UtilizedText/Errors )
    public class AppException : Exception
    {
        public AppExceptionErrorModel ErrorObject { get; set; }

        public AppException(AppExceptionErrorModel ErrorObject)
        {
            this.ErrorObject = ErrorObject;
        }

        public AppException() : base() { }
        public AppException(string message, IConfiguration _configuration) : base(_configuration.GetSection("BackendErrors")[message] ?? message)
        {
            ErrorObject = new AppExceptionErrorModel
            {
                Error = _configuration.GetSection("BackendErrors")[message] ?? message,
                ErrorCode = message,
            };
        }

        public AppException(string message, string _secondMessage, IConfiguration _configuration) : base(_configuration.GetSection("BackendErrors")[message] ?? message)
        {

            if (!string.IsNullOrEmpty(_secondMessage))
            {
                LogError(_secondMessage, message, _configuration);
            }
            ErrorObject = new AppExceptionErrorModel
            {
                Error = _configuration.GetSection("BackendErrors")[message],
                ErrorCode = message,
            };

        }

        public AppException(string message,string customMessage ,string _secondMessage, IConfiguration _configuration) : base(_configuration.GetSection("BackendErrors")[message] ?? message)
        {

            if (!string.IsNullOrEmpty(_secondMessage))
            {
                LogError(_secondMessage, message, _configuration);
            }
            ErrorObject = new AppExceptionErrorModel
            {
                Error = _configuration.GetSection("BackendErrors")[message]+" "+ customMessage,
                ErrorCode = message,
            };

        }

        public static void LogError(string error, string errorCode, IConfiguration _configuration)
        {
            try
            {
                string filepath = _configuration.GetSection("App-Utils")["ErrorsLogPath"];  //Text File Path

                //if (!Directory.Exists(filepath))
                //{
                //    Directory.CreateDirectory(filepath);

                //}
                if (!File.Exists(filepath))
                {

                    File.Create(filepath).Dispose();

                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string loggedError = "Log Written Date: " + " " + DateTime.Now.ToString() + " | Error Message: " + " " + errorCode;
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(loggedError);
                    sw.WriteLine("Error Message: " + error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.Flush();
                    sw.Close();
                }

            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
    }
}

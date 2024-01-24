
using W8_Backend.Data;
using W8_Backend.Data.Entities;
using W8_Backend.Helpers;
using W8_Backend.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace W8_Backend.Services
{
    public class ValidationService : IValidationService
    {

        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
      

        public ValidationService(DataContext context)
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
        public async Task<bool> ValidateEmailFormatAsync(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

       



    }
}










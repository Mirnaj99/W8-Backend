using Microsoft.EntityFrameworkCore;
using W8_Backend.Data;
using W8_Backend.Helpers;
using W8_Backend.Models.UserModels.Input;
using W8_Backend.Services.Interfaces;

namespace W8_Backend.Services
{
    public class InitDB : IInitDB
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public InitDB(DataContext context, IUserService userService)
        {
            //Initializing the required variables, and defining the required services using dependency injection
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile("Utils\\UtilizedText\\Errors\\English_Errors.json", optional: true)
           .AddEnvironmentVariables();
            _configuration = builder.Build();
            _context = context;
            _userService = userService;
        }
        //Function for database initialization
        public async Task InitDatabaseAsync()
        {
            try
            {
                var checkAdmin = await _context.Users.Where(x => x.IsAdmin ).FirstOrDefaultAsync();
                var checkUser = await _context.Users.Where(x => x.IsAdmin == false).FirstOrDefaultAsync();
                //Creating the levven admin object
                if (checkAdmin == null)
                {
                    UserCreateRequest userToAdd = new UserCreateRequest
                    { 
                        FirstName = "W8",
                        LastName = "Admin",
                        IsAdmin= true,
                        UserName = "mirnajml367@gmail.com",
                    };
                    //Using the create user in the user service to create the levven admin
                    await _userService.CreateUserAsync(userToAdd, true);
                }

                if (checkUser == null)
                {
                    UserCreateRequest userToAdd = new UserCreateRequest
                    {
                        FirstName = "Mirna",
                        LastName = "Jammoul",
                        IsAdmin = false,
                        UserName = "mirna@gmail.com",
                    };
                    //Using the create user in the user service to create the levven admin
                    await _userService.CreateUserAsync(userToAdd, true);
                }

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
    }
}

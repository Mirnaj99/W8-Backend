using W8_Backend.Models.UserModels.Input;
using W8_Backend.Models.UserModels.Output;
using W8_Backend.Services.Interfaces;
using W8_Backend.Data.Entities;
using W8_Backend.Data;
using W8_Backend.Helpers;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Data;


namespace W8_Backend.Services
{
    public class UserService : IUserService
    {

        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IValidationService _validationService;
        private readonly string baseUrl;
        private readonly IMapper _mapper;
       

        public UserService(DataContext context,  IMapper mapper, IValidationService validationService)
        {
            //Initializing the required variables, and defining the required services using dependency injection
            var builder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile("Utils\\UtilizedText\\Errors\\English_Errors.json", optional: true)
           .AddEnvironmentVariables();
            _configuration = builder.Build();
            _context = context;
            _mapper = mapper;
            _validationService = validationService;
            //Fetching the microservice base URL from appsettings. The Microservice will be used for all authentication actions.
            baseUrl = _configuration.GetSection("API-URL")["AuthenticationURL"];
        }


        public async Task<UserView> CreateUserAsync(UserCreateRequest model, bool Init)
        {
            try
            {
                //Validating required fields
                if (string.IsNullOrEmpty(model.FirstName))
                    throw new AppException("err116", _configuration);
                if (string.IsNullOrEmpty(model.LastName))
                    throw new AppException("err117", _configuration);
           
              
                if (string.IsNullOrEmpty(model.UserName))
                    throw new AppException("err122", _configuration);



                //validating if email is valid
                bool isEmailValid = await _validationService.ValidateEmailFormatAsync(model.UserName);
                if (!isEmailValid)
                    throw new AppException("err39", _configuration);
                //Checking if the user already exists            
                var checkUser = await _context.Users.SingleOrDefaultAsync(e => e.UserName == model.UserName);
                if (checkUser != null)
                    throw new AppException("err367", _configuration);

              
                var checkModel = new GetByEmailRequest
                {
                    Email = model.UserName,
                };
                var urlRegister = baseUrl + "/Accounts/register";
                var urlCheckEmail = baseUrl + "/accounts/getByEmail";
                var client = new HttpClient();
                var response = client.PostAsJsonAsync(urlCheckEmail, checkModel).Result;
                Users newProfile = new Users
                {
                    UserName = model.UserName.ToLower().Trim(),
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsAdmin = model.IsAdmin,

                   
                };
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //If the user was found on the microservice we add him/her on the w8 database directly
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    var content = JsonConvert.DeserializeObject<UserResponse>(contentResponse);
                    newProfile.MicroUserID = content.Id;
                    await _context.Users.AddAsync(newProfile);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //If the user is not on the microservice, we create the model to add him to the w8 database
                    AuthenticationServiceRegisterRequest authenticationServiceCreateRequest = new AuthenticationServiceRegisterRequest
                    {
                        AppName = "W8",
                        Email = model.UserName,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserName = model.UserName,
                        DoNotSendVerificationEmail = true,
                        VerifiedUser = true,
                        AcceptTerms = true
                    };
                  
                        authenticationServiceCreateRequest.Password = "Admin@123";
                        authenticationServiceCreateRequest.ConfirmPassword = "Admin@123";
                    
                    
                    //After the user is added successfully to the microservice, we add him/her to the w8 database
                    var response2 = await client.PostAsJsonAsync(urlRegister, authenticationServiceCreateRequest);
                    if (response2.StatusCode == HttpStatusCode.OK)
                    {
                        var contentResponse = await response2.Content.ReadAsStringAsync();
                        var content = JsonConvert.DeserializeObject<UserResponse>(contentResponse);
                        newProfile.MicroUserID = content.Id;
                        await _context.Users.AddAsync(newProfile);

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        //Returning an error if adding the user to the microservice failed
                        //var json = await response2.Content.ReadAsStringAsync();
                        //var token = JObject.Parse(json).GetValue("message");
                        //throw new AppException("err000", token == null ? "" :  token.ToString(), _configuration);
                        var contentResponse = await response2.Content.ReadAsStringAsync();
                        var content = JsonConvert.DeserializeObject<Error>(contentResponse);
                        throw new AppException("err000", content.Message, _configuration);
                    }
                }

                //Returning the result model containing the user and the role objects
                Users user = await _context.Users.SingleOrDefaultAsync(u => u.UserName.Equals(model.UserName));
                UserView toBeReturned = new UserView
                {
                    User = user,
                    IsAdmin = user.IsAdmin
                };
                return toBeReturned;
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
                    case 2601:
                        // Unique Index/Constraint Violation
                        if (s.Contains("MicroUserID"))
                        {
                            error = "err88";
                        }
                        else if (s.Contains("UserName"))
                        {
                            error = "err87";
                        }
                        else
                        {
                            error = "err84";
                            scndMsg = err == null ? "" : err.Message;
                        }

                        break;
                   
                    default:
                        // throw a general db Exception
                        error = "err84";
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

        //This function is called on logout to delete the saved refresh token from the cookies thus forcing the user to login for accessing his account again
        public async Task DeleteCookiesAsync(HttpResponse Response)
        {
            try
            {

                CookieOptions option = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(-8)
                };
                Response.Cookies.Append("refreshToken", "delete token", option);
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

        //Function to get user by ID
        public async Task<UserView> GetUserByIdAsync(int userID)
        {
            try
            {
                var getUser = await _context.Users.SingleOrDefaultAsync(u => u.UserID == userID);
                if (getUser == null)
                    throw new AppException("err1", _configuration);
                UserView toBeReturned = new UserView
                {
                    User = getUser,
                    IsAdmin = getUser.IsAdmin,
                };
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
        public async Task<bool> IsTokenActiveAsync(CheckIfTokenIsExpiredRequest model)
        {
            try
            {
                //Validating the sent model
                var url = baseUrl + "/accounts/CheckIfTokenIsActive";
                var client = new HttpClient();
                //Performing the call on the microservice to check if the token is valid and returning the result based on the response
                var response = await client.PostAsJsonAsync(url, model);
                if (!response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    var content = JsonConvert.DeserializeObject<Error>(contentResponse);
                    throw new AppException(content.Message, _configuration);
                }
                else
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    var content = JsonConvert.DeserializeObject<bool>(contentResponse);
                    if (content == true)
                    {
                        return true;
                    }
                    else
                    {
                        throw new AppException("err340", _configuration);
                    }
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

        //Sending request with credentials for microservice to login and give user a token (user agent is used to check if the device the user is using  is new).
        public async Task<UserView> LoginAsync(AuthenticateRequest authenticateRequest, HttpResponse Response, HttpRequest request)
        {
            try
            {
                //Validating the fields in the model
                if (!string.IsNullOrEmpty(authenticateRequest.Email))
                {
                    authenticateRequest.Email = authenticateRequest.Email.ToLower();
                    authenticateRequest.Email = authenticateRequest.Email.Trim();
                }
                bool isEmailValid = await _validationService.ValidateEmailFormatAsync(authenticateRequest.Email);
                if (!isEmailValid)
                    throw new AppException("err39", _configuration);
                //Adding the user-agent to the model to verify if the device used is new or not
               // authenticateRequest.DeviceInfo = request.Headers["User-Agent"].ToString();
                var getUser = await _context.Users.SingleOrDefaultAsync(u => u.UserName == authenticateRequest.Email);
                if (getUser == null)
                    throw new AppException("err1", _configuration);

             

                var url = baseUrl + "/accounts/authenticate";
                var client = new HttpClient();
                authenticateRequest.AppName = "W8";
                //Sending the request to the microservice and returning the result model
                var response = client.PostAsJsonAsync(url, authenticateRequest).Result;
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    var content = JsonConvert.DeserializeObject<AuthenticateResponse>(contentResponse);
                    //SetTokenCookie(content.RefreshToken, Response);
                    var userView = new UserView
                    {
                        User = getUser,
                        JwtToken = content.JwtToken,
                        RefreshToken = content.RefreshToken,
                        IsAdmin = content.isAdmin
                       
                    };




                    return userView;

                }
                else
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    var content = JsonConvert.DeserializeObject<Error>(contentResponse);
                    throw new AppException(content.Message, _configuration);
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

        //Performing a call to microservice using the current valid refresh token to refresh the user info and jwt token so he remains authenticated
        public async Task<UserView> RefreshTokenAsync(RefreshTokenRequest model)
        {
            try
            {
                var url = baseUrl + "/accounts/refresh-token";
                var client = new HttpClient();
                var refreshToken = model.Token;
                //Getting the refresh token and sending the request to the microservice to refresh the user token. After that the result model is returned with the refreshed user info.
                RefreshTokenRequest data = new RefreshTokenRequest
                {
                    Token = refreshToken
                };
                var ReturnedResponse = await client.PostAsJsonAsync(url, data);
                if (ReturnedResponse == null)
                    throw new AppException("err5", _configuration);
                var response = ReturnedResponse;
                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    var content = JsonConvert.DeserializeObject<AuthenticateResponse>(contentResponse);
                    //SetTokenCookie(content.RefreshToken, Response);

                    var getUser = await _context.Users.SingleOrDefaultAsync(u => u.MicroUserID == content.Id);
                    if (getUser == null)
                        throw new AppException("err1", _configuration);


                    var userView = new UserView
                    {
                        User = getUser,
                        JwtToken = content.JwtToken,
                        RefreshToken = content.RefreshToken,
                        IsAdmin = content.isAdmin
                    };

                    return userView;
                }
                else
                {
                    throw new AppException(response.ReasonPhrase, _configuration);
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
        //Function that checks if the user with the email from the model exists on the microservice database
        public async Task<bool> CheckEmailAsync(GetByEmailRequest model)
        {
            try
            {
                //Validating if email is empty
                if (string.IsNullOrEmpty(model.Email))
                    throw new AppException("err8", _configuration);
                bool isEmailValid = await _validationService.ValidateEmailFormatAsync(model.Email);
                if (!isEmailValid)
                    throw new AppException("err39", _configuration);
                string getUserByEmailURL = baseUrl + "/accounts/checkEmail";
                HttpClient client = new HttpClient();
                //Sending a request to microservice to check if a user with the email from the model exists on the microservice database
                var response = await client.PostAsJsonAsync(getUserByEmailURL, model);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    return true;
                }
                else return false;
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

        //Function for revoking the token from the user that owns the token 
        public async Task RevokeTokenAsync(RevokeTokenRequest revokeTokenModel, HttpRequest Request)
        {
            try
            {
                // accept token from request body or cookie
                var token = revokeTokenModel.Token ?? Request.Cookies["refreshToken"];

                if (string.IsNullOrEmpty(token))
                    throw new AppException("err127", _configuration);

                var url = baseUrl + "/accounts/revoke-token";
                var client = new HttpClient();
                var response = await client.PostAsJsonAsync(url, revokeTokenModel);
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

        //Function to update a user record
        public async Task<int> UpdateUserAsync(int userID, UserUpdateRequest model)
        {
            try
            {
                //Checking if the user to update exists in the database
                Users userToUpdate = await _context.Users.FindAsync(userID);
                if (userToUpdate == null)
                    throw new AppException("err1", _configuration);
                var checkModifier = await _context.Users.FindAsync(model.ModifiedByID);
                if (checkModifier == null)
                    throw new AppException("err53", _configuration);
                //Mapping the model to the retrieved record from the database
                userToUpdate = _mapper.Map(model, userToUpdate);
               

                //Validating required fields
                if (string.IsNullOrEmpty(userToUpdate.FirstName))
                    throw new AppException("err116", _configuration);
                if (string.IsNullOrEmpty(userToUpdate.LastName))
                    throw new AppException("err117", _configuration);
                if (string.IsNullOrEmpty(userToUpdate.PhoneNumber))
                    throw new AppException("err119", _configuration);


                //Updating the record in the database
                _context.Users.Update(userToUpdate);

                await _context.SaveChangesAsync();
                return userToUpdate.UserID;
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
                string scndMsg = "";

                string error = "";
                switch (number)
                {
                    case 2601:
                        // Unique Index/Constraint Violation
                        if (s.Contains("MicroUserID"))
                        {
                            error = "err88";
                        }
                        else if (s.Contains("UserName"))
                        {
                            error = "err87";
                        }
                        else
                        {
                            error = "err85";
                            scndMsg = err == null ? "" : err.Message;
                        }

                        break;
                  
                       
                    default:
                        // throw a general db Exception
                        error = "err85";
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

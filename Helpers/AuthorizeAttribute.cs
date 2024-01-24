using W8_Backend.Data;
using W8_Backend.Models.CommonModels.Input;
using W8_Backend.Models.ErrorModels.Output;
using W8_Backend.Models.UserModels.Output;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;


using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace W8_Backend.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute
        : Attribute, IAuthorizationFilter
    {

        private readonly IConfiguration _configuration;
        private readonly string userID;

        private readonly string baseUrl;
        private readonly string _permissions;




        public AuthorizeAttribute(string permissions)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("Utils\\UtilizedText\\Errors\\English_Errors.json", optional: true)
            .AddEnvironmentVariables();
            _configuration = builder.Build();
            userID = "MicroUserID";
            baseUrl = _configuration.GetSection("API-URL")["AuthenticationURL"];
            _permissions = permissions;
        }

        public AuthorizeAttribute()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("Utils\\UtilizedText\\Errors\\English_Errors.json", optional: true)
            .AddEnvironmentVariables();
            _configuration = builder.Build();
            userID = "MicroUserID";
            baseUrl = _configuration.GetSection("API-URL")["AuthenticationURL"];
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            AppExceptionErrorModel ErrorObject = new AppExceptionErrorModel
            {
                Error = _configuration.GetSection("BackendErrors")["errUnauth"] ?? "errUnauth",
                ErrorCode = "errUnauth",
            };
            StringValues token = "";
            var ht = context.HttpContext.Request.Headers.TryGetValue("Authorization", out token); ;
            if (StringValues.IsNullOrEmpty(token))
                context.Result = new JsonResult(new { message = ErrorObject.Error, error = ErrorObject })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            else
            {
                var t = token.ToString();
                if (t.Length < 7)
                    context.Result = new JsonResult(new { message = ErrorObject.Error, error = ErrorObject })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                int l = t.Length;
                t = t.Substring(7, l - 7);
                var userInfoUrl = baseUrl + "/accounts/current-user";
                var hc = new HttpClient();
                hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", t);
                var response = hc.GetAsync(userInfoUrl).Result;
                if (response == null)
                    context.Result = new JsonResult(new { message = ErrorObject.Error, error = ErrorObject })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                if (response.StatusCode != HttpStatusCode.OK)
                    context.Result = new JsonResult(new { message = ErrorObject.Error, error = ErrorObject })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                else
                {
                    var account = response.Content.ReadFromJsonAsync<AuthenticateResponse>().Result;
                    var db = context.HttpContext.RequestServices.GetRequiredService<DataContext>();
                    var currentUser = db.Users.AsNoTracking().SingleOrDefaultAsync(x => x.MicroUserID == account.Id).Result;
                  
                   
                    try
                    {
                        if (!string.IsNullOrEmpty(_permissions)) 
                        {
                            string permissions = "";
                            string userCurrentRole = currentUser.IsAdmin ? "Admin" : "User";

                            if (!string.IsNullOrEmpty(_permissions))
                            {
                                permissions = _permissions;
                            }

                            if (!permissions.Contains(userCurrentRole))
                            {
                                context.Result = new JsonResult(new { message = ErrorObject.Error, error = ErrorObject })
                                {
                                    StatusCode = StatusCodes.Status401Unauthorized
                                };
                            }



                            var claims = new List<Claim>
                        {
                        new Claim(userID, currentUser.MicroUserID)
                        };
                            var appIdentity = new ClaimsIdentity(claims);
                            context.HttpContext.User.AddIdentity(appIdentity);
                        }
                    }
                    catch (Exception e)
                    {
                        context.Result = new JsonResult(new { message = ErrorObject.Error, error = ErrorObject })
                        {
                            StatusCode = StatusCodes.Status401Unauthorized
                        };
                        var claims = new List<Claim>
                        {
                            new Claim(userID, currentUser.MicroUserID)
                        };
                        var appIdentity = new ClaimsIdentity(claims);
                        context.HttpContext.User.AddIdentity(appIdentity);
                    }
                }
            }
        }
    }
}

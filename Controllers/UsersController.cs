
using W8_Backend.Helpers;
using W8_Backend.Models.UserModels.Input;
using W8_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace W8_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _usersService;
        public UsersController(
           IUserService usersService
           )
        {
            _usersService = usersService;

        }



        [HttpPost("CheckEmail")]
        public async Task<IActionResult> GetUserByEmailAsync(GetByEmailRequest model)
        {
            try
            {
                //Calls the function that checks user email in database
                var result = await _usersService.CheckEmailAsync(model);
                return Ok(result);
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(AuthenticateRequest authenticateRequest)
        {
            try
            {
                //Calls the login function 
                var result = await _usersService.LoginAsync(authenticateRequest, Response, Request);
                return Ok(result);
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }



        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest model)
        {
            try
            {
                //Calls the function that refreshes the user token
                var res = await _usersService.RefreshTokenAsync(model);
                return Ok(res);
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }



        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeTokenRequest model)
        {
            try
            {
                //Calls the function revoke-token
                await _usersService.RevokeTokenAsync(model, Request);
                return Ok(new { message = "Token revoked" });
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }
        [HttpPost("deleteCookies")]
        public async Task<IActionResult> DeleteCookiesAsync()
        {
            try
            {
                //Calls the delete cookies function
                await _usersService.DeleteCookiesAsync(Response);
                return Ok(new { message = "Cookie Deleted" });
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }

        [Authorize]
        [HttpPost("UpdateUser/{userID}")]
        public async Task<IActionResult> UpdateUserAsync(int userID, UserUpdateRequest model)
        {
            try
            {
                //Function that updates a user
                var result = await _usersService.UpdateUserAsync(userID, model);

                return Ok(result);
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }
        [Authorize]
        [HttpGet("GetUserByID/{userID}")]
        public async Task<IActionResult> GetUserByIDAsync(int userID)
        {
            try
            {
                //Function that gets user by ID
                var result = await _usersService.GetUserByIdAsync(userID);

                return Ok(result);
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }


        [HttpPost("IsTokenActive")]
        public async Task<IActionResult> IsTokenActiveAsync(CheckIfTokenIsExpiredRequest model)
        {
            try
            {
                //Calls the function that checks if a token is active or expired
                var result = await _usersService.IsTokenActiveAsync(model);
                return Ok(result);
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }
    }
}

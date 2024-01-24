

using Microsoft.AspNetCore.Mvc;
using W8_Backend.Helpers;
using W8_Backend.Services.Interfaces;

namespace W8_Backend.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class InitDBController : Controller
    {

        private readonly IInitDB _initService;
        public InitDBController(
        IInitDB initService

        )
        {
            _initService = initService;

        }


        [HttpGet("InitDb")]
        public async Task<IActionResult> InitDb()
        {
            try
            {
                //Calls the function that initializes the database 
                await _initService.InitDatabaseAsync();
                return Ok();
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }
    }
}

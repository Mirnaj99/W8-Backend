using W8_Backend.Helpers;
using W8_Backend.Models.LogModels.Input;
using W8_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace W8_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LogsController : Controller
    {
        private readonly ILogsService _logsService;
        public LogsController(
        ILogsService logsService

        )
        {
            _logsService = logsService;

        }

        [HttpPost("GetLogsList")]
        public async Task<IActionResult> GetLogsListAsync(GetLogsListRequest model)
        {
            try
            {
                //Calls the function that gets a list of logs
                var result = await _logsService.GetLogsListAsync(model);
                return Ok(result);
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }

        }

        [HttpPost("GetLogDetailsList")]
        public async Task<IActionResult> GetLogDetaisListAsync(GetLogDetailsRequest model)
        {
            try
            {
                //Calls the function that gets a list of details for a certain log
                var result = await _logsService.GetLogDetailsListAsync(model);
                return Ok(result);
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }

        }
        [HttpPost("DeleteLogsList")]
        public async Task<IActionResult> DeleteLogsListAsync(DeleteLogsRequest model)
        {
            try
            {
                //Calls the function that deletes a list of logs
                await _logsService.DeleteLogsListAsync(model);
                return Ok();
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }

        }
        [HttpPost("DeleteLogDetailsList")]
        public async Task<IActionResult> DeleteLogDetailsListAsync(DeleteLogDetailsRequest model)
        {
            try
            {
                //Calls the function that deletes a list of details for a certain log
                await _logsService.DeleteLogDetailsListAsync(model);
                return Ok();
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }

        }
    }
}

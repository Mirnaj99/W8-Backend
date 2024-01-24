using Microsoft.AspNetCore.Mvc;
using W8_Backend.Helpers;
using W8_Backend.Models.FileModels;
using W8_Backend.Models.SystemVariables;
using W8_Backend.Services.Interfaces;

namespace W8_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SystemVariablesController : Controller
    {
        private readonly ISystemVariablesService _systemVariablesService;
        private readonly IFilesService _filesService;
        public SystemVariablesController(ISystemVariablesService systemVariablesService, IFilesService filesService)
        {
            _systemVariablesService = systemVariablesService;
            _filesService = filesService;
        }
        [HttpGet("GetSystemVariablesData")]
        public async Task<IActionResult> GetSystemVariablesDataAsync()
        {
            try
            {
                //Calls the function that returns all systemVariables data
                var result = await _systemVariablesService.GetSystemVariablesDataAsync();

                return Ok(result);
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }

        [HttpPost("UpdateMiddlewareFields")]
        public async Task<IActionResult> UpdateLogCleanupAsync(UpdateMiddlewareFields model)
        {
            try
            {
                //Calls the function that updates Middleware variables
                await _systemVariablesService.UpdateMiddlewareFieldsAsync(model);

                return Ok();
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }

        [HttpPost("UploadSheetAndSync")]
        public async Task<IActionResult> UploadSheetAndSync([FromForm] FileModel model)
        {
            try
            {
                //Calls the function that updates Middleware variables
                await _systemVariablesService.UpdateMiddlewareSyncMonthlyCostSheetFieldAsync();
                await _filesService.UploadFileAsync(model);

                return Ok();
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }


        [HttpPost("UpdateHRFieldsAsync")]
        public async Task<IActionResult> UpdateHRFieldsAsync(UpdateMiddlewareFields model)
        {
            try
            {
                //Calls the function that updates Middleware variables
                await _systemVariablesService.UpdateHRFieldsAsync(model);

                return Ok();
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }

        [HttpPost("UpdateSyncDateFieldAsync")]
        public async Task<IActionResult> UpdateSyncDateFieldAsync(UpdateMiddlewareFields model)
        {
            try
            {
                //Calls the function that updates Middleware variables
                await _systemVariablesService.UpdateSyncDateFieldAsync(model);

                return Ok();
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }

        [HttpPost("UpdateDeleteRecordsFieldsAsync")]
        public async Task<IActionResult> UpdateDeleteRecordsFieldsAsync(UpdateMiddlewareFields model)
        {
            try
            {
                //Calls the function that updates Middleware variables
                await _systemVariablesService.UpdateDeleteRecordsFieldsAsync(model);

                return Ok();
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }


        [HttpPost("UpdateMiddlewareSyncMonthlyCostSheetFieldAsync")]
        public async Task<IActionResult> UpdateMiddlewareSyncMonthlyCostSheetFieldAsync()
        {
            try
            {
                //Calls the function that updates Middleware variables
                await _systemVariablesService.UpdateMiddlewareSyncMonthlyCostSheetFieldAsync();

                return Ok();
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }


        [HttpPost("UpdateSyncEmpDiffFieldsAsync")]
        public async Task<IActionResult> UpdateSyncEmpDiffFieldsAsync()
        {
            try
            {
                //Calls the function that updates Middleware variables
                await _systemVariablesService.UpdateSyncEmpDiffFieldsAsync();

                return Ok();
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }
        }
    }
}





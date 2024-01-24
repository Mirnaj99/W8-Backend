using Microsoft.AspNetCore.Mvc;
using W8_Backend.Helpers;
using W8_Backend.Services.Interfaces;
using W8_Backend.Models.FileModels;
using Microsoft.AspNetCore.Mvc;
using W8_Backend.Services;

namespace W8_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class FilesController :  Controller
    {

        private readonly IFilesService _filesService;
        public FilesController( IFilesService filesService)
        {
            _filesService = filesService;

        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFileAsync([FromForm]FileModel model)
        {
            try
            {
                //Calls the function that gets a list of logs
                var result = await _filesService.UploadFileAsync(model);
                return Ok(result);
            }
            catch (AppException e)
            {
                return BadRequest(new { message = e.Message, error = e.ErrorObject });
            }

        }
    }
}

using W8_Backend.Helpers;
using W8_Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace W8_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValidationController : Controller
    {
        private readonly IValidationService _validationService;

        public ValidationController(
           IValidationService validationService
           )
        {
            _validationService = validationService;
        }

        
    }
}

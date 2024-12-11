using HttpRequestHandling.RefitInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace HttpRequestHandling.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RefitController : ControllerBase
    {
        #region Private properties and Constructors 
        private readonly ILogger<RefitController> _logger;
        private readonly IWeatherServiceForRefit _weatherService;

        public RefitController(ILogger<RefitController> logger, IWeatherServiceForRefit weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }
        #endregion

        [HttpGet("/Refit_Route")]
        public async Task<IActionResult> Refit_Route(string cityName = "Noida")
        {
            #region Validate and Sanitize arguements
            #endregion

            var data = await _weatherService.GetByCityNameR(cityName);
            return Ok(data);
        }

        [HttpGet("/Refit_Query")]
        public async Task<IActionResult> Refit_Query(string cityName = "Noida")
        {
            #region Validate and Sanitize arguements
            #endregion

            var data = await _weatherService.GetByCityNameQ(cityName);
            return Ok(data);
        }
        
        [HttpGet("/Refit_AutoDeserialization")]
        public async Task<IActionResult> Refit_AutoDeserialization(string cityName = "Noida")
        {
            #region Validate and Sanitize arguements
            #endregion

            var data = await _weatherService.GetByCityNameAndAutoParse(cityName);
            return Ok(data);
        }
        
        [HttpGet("/Refit_GenericResponse")]
        public async Task<IActionResult> Refit_GenericResponse(string cityName = "Noida")
        {
            #region Validate and Sanitize arguements
            #endregion

            var response = await _weatherService.GetByCityName(cityName);
            if (response.IsSuccessful)
                return Ok(response.Content);
            else 
                return StatusCode((int)response.StatusCode, new { message = response.Error.Message });
        }
    }
}

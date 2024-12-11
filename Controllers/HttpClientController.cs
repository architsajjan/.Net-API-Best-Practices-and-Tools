using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HttpRequestHandling.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class HttpClientController : ControllerBase
    {
        #region Private properties and Constructors 
        private readonly ILogger<HttpClientController> _logger;
        private readonly string _apiKey = "4d3e7e19c9254e03962144751242110";

        private static HttpClient _httpClient;

        static HttpClientController()
        {
            _httpClient = new HttpClient();
        }

        public HttpClientController(ILogger<HttpClientController> logger)
        {
            _logger = logger;
        }
        #endregion

        [HttpGet("/HttpClient_BasicGetCall")]
        public async Task<string> BasicGetCallWithHttpClient(string cityName="Noida")
        {
            #region Validate and Sanitize arguements
            #endregion

            var URI = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={cityName}";
            var httpClient = new HttpClient();
            
            var response = await httpClient.GetAsync(URI);
            return await response.Content.ReadAsStringAsync();
        }
        
        [HttpGet("/HttpClient_InsideUsingBlock")]
        public async Task<string> HttpClientInsideUsing(string cityName = "Noida")
        {
            #region Validate and Sanitize arguements
            #endregion

            var URI = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={cityName}";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(URI);
                return await response.Content.ReadAsStringAsync();
            }
        }
        
        [HttpGet("/HttpClient_UsingSingletonInstance_1")]
        public async Task<string> HttpClientUsingSingletonInstance_1(string cityName = "Noida")
        {
            #region Validate and Sanitize arguements
            #endregion

            var URI = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={cityName}";
            
            var response = await _httpClient.GetAsync(URI);
            return await response.Content.ReadAsStringAsync();
        }
        
        [HttpGet("/HttpClient_UsingSingletonInstance_2")]
        public async Task<string> HttpClientUsingSingletonInstance_2(string cityName = "Delhi")
        {
            #region Validate and Sanitize arguements
            #endregion

            var URI = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={cityName}";
            
            var response = await _httpClient.GetAsync(URI);
            return await response.Content.ReadAsStringAsync();
        }

        #region Restsharp Comparision
        [HttpGet("/HttpClient_Batch")]
        public async Task<IActionResult> HttpClient_Batch([FromQuery] string[] cityNames)
        {
            #region Validate and Sanitize arguements
            #endregion

            var tasks = new List<Task<WeatherResponse>>();

            foreach (var cityName in cityNames)
            {
                // Construct the request URL
                var URI = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={cityName}";

                tasks.Add(_httpClient.GetStringAsync(URI).ContinueWith(responseTask =>
                {
                    if (responseTask.IsCompletedSuccessfully)
                    {
                        return JsonSerializer.Deserialize<WeatherResponse>(responseTask.Result);
                    }
                    else
                    {
                        return null; // Handle error case appropriately
                    }
                }) as Task<WeatherResponse>);
            }

            var responses = await Task.WhenAll(tasks);

            var validResponses = responses.Where(r => r != null).ToArray();

            return Ok(validResponses); // This will return an array of WeatherResponse
        }
        #endregion
    }
}

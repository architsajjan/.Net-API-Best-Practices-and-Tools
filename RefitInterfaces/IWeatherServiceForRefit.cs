using Refit;

namespace HttpRequestHandling.RefitInterfaces
{
    [Headers("Accept: application/json")]
    public interface IWeatherServiceForRefit
    {
        [Get("/current.json?key=4d3e7e19c9254e03962144751242110")]
        Task<string> GetByCityNameQ(string q);

        [Get("/current.json?key=4d3e7e19c9254e03962144751242110&q={cityName}")]
        Task<string> GetByCityNameR(string cityName);

        [Get("/current.json?key=4d3e7e19c9254e03962144751242110&q={cityName}")]
        Task<WeatherResponse> GetByCityNameAndAutoParse(string cityName);

        [Get("/current.json?key=4d3e7e19c9254e03962144751242110&q={cityName}")]
        [Headers("Accept: application/json")]
        Task<ApiResponse<WeatherResponse>> GetByCityName(string cityName);
    }
}
using Application.Contracts.Dto.WeatherForecast.Request;
using Application.Contracts.Dto.WeatherForecast.Response;

namespace Application.Mapper
{
    /// <summary>
    /// Class map for WeatherForecast
    /// </summary>
    public class MapWeatherForecast : Profile
    {
        public MapWeatherForecast()
        {
            CreateMap<RequestCreateWeatherForecastDto, WeatherForecast>();
            CreateMap<RequestUpdateWeatherForecastDto, WeatherForecast>();
            CreateMap<WeatherForecast, ResponseGetWeatherForecastDto>();
        }
    }
}

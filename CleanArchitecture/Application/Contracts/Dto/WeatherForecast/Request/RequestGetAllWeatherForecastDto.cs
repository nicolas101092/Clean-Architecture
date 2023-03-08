using Cross_Cutting.Pagination.Dto;

namespace Application.Contracts.Dto.WeatherForecast.Request
{
    public class RequestGetAllWeatherForecastDto : DtoListFiltersBase
    {
        public string OrderBy { get; set; } = "Id";
        public bool IsDescending { get; set; }
    }
}

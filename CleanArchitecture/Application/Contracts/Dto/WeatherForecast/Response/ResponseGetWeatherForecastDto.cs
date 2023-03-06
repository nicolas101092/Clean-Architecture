namespace Application.Contracts.Dto.WeatherForecast.Response
{
    public class ResponseGetWeatherForecastDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string? Summary { get; set; }
    }
}

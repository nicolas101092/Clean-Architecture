using FluentValidation;

namespace Application.Contracts.Dto.WeatherForecast.Request
{
    public class RequestUpdateWeatherForecastDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }
    }

    public class RequestUpdateWeatherForecastDtoValidator : AbstractValidator<RequestUpdateWeatherForecastDto>
    {
        public RequestUpdateWeatherForecastDtoValidator()
        {
            RuleFor(x => x.Summary).NotEmpty().MaximumLength(450);
        }
    }
}

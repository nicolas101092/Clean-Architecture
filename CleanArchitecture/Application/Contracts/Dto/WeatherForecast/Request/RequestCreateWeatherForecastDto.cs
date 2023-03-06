using FluentValidation;

namespace Application.Contracts.Dto.WeatherForecast.Request
{
    public class RequestCreateWeatherForecastDto
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }
    }

    public class RequestCreateWeatherForecastDtoValidator : AbstractValidator<RequestCreateWeatherForecastDto>
    {
        public RequestCreateWeatherForecastDtoValidator()
        {
            RuleFor(x => x.Summary).NotEmpty().MaximumLength(450);
        }
    }
}

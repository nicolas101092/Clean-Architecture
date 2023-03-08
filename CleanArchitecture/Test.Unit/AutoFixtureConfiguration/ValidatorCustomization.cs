using Application.Contracts.Dto.WeatherForecast.Request;
using FluentValidation;

namespace Test.Unit.AutoFixtureConfiguration
{
    /// <summary>
    /// Validator customization
    /// </summary>
    public class ValidatorCustomization : ICustomization
    {
        /// <summary>
        /// Add new Validator registrations
        /// </summary>
        /// <param name="fixture">Fixture</param>
        public void Customize(IFixture fixture)
        {
            fixture.Register<IValidator<RequestCreateWeatherForecastDto>>(() => new RequestCreateWeatherForecastDtoValidator());
            fixture.Register<IValidator<RequestUpdateWeatherForecastDto>>(() => new RequestUpdateWeatherForecastDtoValidator());
        }
    }
}

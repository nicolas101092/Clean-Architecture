using Application.Contracts.Dto.WeatherForecast.Request;
using Application.Services.WeatherForecastService;
using Cross_Cutting.Extensions.ExceptionExtensions;
using Domain;
using Domain.Abstract.Repositories;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Test.Unit
{
    public class WeatherForecastServiceTest
    {
        [Theory, AutoFixture]
        internal async Task Test_should_be_created_when_insert_a_new_WeatherForecast(
            [Frozen] Mock<IWeatherForecastRepository> repository,
            WeatherForecastService sut,
            RequestCreateWeatherForecastDto request,
            WeatherForecast expectedResponse)
        {
            // Arrange
            repository
                .Setup(c => c.AddAsync(It.IsAny<WeatherForecast>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await sut.CreateAsync(request);

            // Assert
            result.Id.Should().Be(expectedResponse.Id);
            result.TemperatureC.Should().Be(expectedResponse.TemperatureC);
            result.TemperatureF.Should().Be(expectedResponse.TemperatureF);
        }

        [Theory, AutoFixture]
        internal async Task Test_should_throw_BadRequestException_when_insert_a_new_WeatherForecast_with_validation_errors(
            WeatherForecastService sut,
            RequestCreateWeatherForecastDto request)
        {
            // Arrange
            request.Summary = string.Empty;

            // Act
            // Assert
            await Assert.ThrowsAsync<BadRequestException>(async () => await sut.CreateAsync(request));
        }
    }
}
using Application.Contracts.Dto.WeatherForecast.Request;
using Application.Contracts.Dto.WeatherForecast.Response;
using Cross_Cutting.Pagination.Dto;

namespace Application.Services.WeatherForecastService
{
    /// <summary>
    /// WeatherForecast service's interface
    /// </summary>
    public interface IWeatherForecastService
    {
        /// <summary>
        /// Creates a new WeatherForecast into database
        /// </summary>
        /// <param name="request">Dto with the requested object</param>
        /// <returns>Dto with the response object</returns>
        Task<ResponseGetWeatherForecastDto> CreateAsync(RequestCreateWeatherForecastDto request);

        /// <summary>
        /// Obtains a list of WeatherForecast ordered and paginated from database
        /// </summary>
        /// <param name="request">Dto with the requested object</param>
        /// <returns>Dto with the response object</returns>
        Task<DtoBasePagination<ResponseGetWeatherForecastDto>> GetListAsync(RequestGetAllWeatherForecastDto request);

        /// <summary>
        /// Obtains a WeatherForecast filtered by Id from database
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Dto with the response object</returns>
        Task<ResponseGetWeatherForecastDto> GetAsync(int id);

        /// <summary>
        /// Removes a WeatherForecast from database
        /// </summary>
        /// <param name="id">Identifier</param>
        Task RemoveAsync(int id);

        /// <summary>
        /// Updated a WeatherForecast into database
        /// </summary>
        /// <param name="request">Dto with the requested object</param>
        Task UpdateAsync(RequestUpdateWeatherForecastDto request);
    }
}

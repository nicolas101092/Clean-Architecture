using Application.Contracts.Dto.WeatherForecast.Request;
using Application.Contracts.Dto.WeatherForecast.Response;
using Application.Services.WeatherForecastService;

namespace CleanArchitecture.API.Controllers
{
    /// <summary>
    /// Specific WeatherForecast controller that inherit from baseController
    /// </summary>
    public class WeatherForecastController : BaseController
    {
        private readonly IWeatherForecastService _weatherForecastService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="weatherForecastService">weatherForecastService service for use in this class</param>
        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        /// <summary>
        /// Create a WeatherForecast
        /// </summary>
        /// <remarks>
        /// Create a WeatherForecast with follow data: <br/>
        /// - Date: DateTime with format YYYY:MM:DD. <br/>
        /// - TemperatureC: Numeric positive and negative value.<br/>
        /// - Summary: Text description. <br/>
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseGetWeatherForecastDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(RequestCreateWeatherForecastDto request)
        {
            var result = await _weatherForecastService.CreateAsync(request);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        /// <summary>
        /// Get a WeatherForecast
        /// </summary>
        /// <remarks>
        /// Get  WeatherForecast with a parameter: <br/>
        /// - Id: Numeric identifier value.<br/>
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseGetWeatherForecastDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _weatherForecastService.GetAsync(id);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        /// <summary>
        /// Get a list of WeatherForecast ordered and paginated
        /// </summary>
        /// <remarks>
        /// Get a list of WeatherForecast in database<br/>
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseGetWeatherForecastDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetList([FromQuery] RequestGetAllWeatherForecastDto request)
        {
            var response = await _weatherForecastService.GetListAsync(request);

            if (response == null)
                return NoContent();

            return Ok(response);
        }

        /// <summary>
        /// Get a WeatherForecast
        /// </summary>
        /// <remarks>
        /// Get  WeatherForecast with a parameter: <br/>
        /// - Id: Numeric identifier value.<br/>
        /// - Date: DateTime with format YYYY:MM:DD. <br/>
        /// - TemperatureC: Numeric positive and negative value.<br/>
        /// - Summary: Text description. <br/>
        /// </remarks>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseGetWeatherForecastDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, RequestUpdateWeatherForecastDto request)
        {
            request.Id = id;
            await _weatherForecastService.UpdateAsync(request);

            return NoContent();
        }

        /// <summary>
        /// Delete a WeatherForecast
        /// </summary>
        /// <remarks>
        /// Remove  WeatherForecast with a parameter from a data base: <br/>
        /// - Id: Numeric identifier value.<br/>
        /// </remarks>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _weatherForecastService.RemoveAsync(id);

            return NoContent();
        }
    }
}
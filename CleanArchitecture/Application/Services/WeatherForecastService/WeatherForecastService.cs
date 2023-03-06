using Application.Contracts.Dto.WeatherForecast.Request;
using Application.Contracts.Dto.WeatherForecast.Response;
using Cross_Cutting.Extensions;
using Cross_Cutting.Pagination.Dto;
using FluentValidation;

namespace Application.Services.WeatherForecastService
{
    /// <summary>
    /// WeatherForecast's service
    /// </summary>
    internal class WeatherForecastService : IWeatherForecastService
    {
        #region Properties

        private readonly IWeatherForecastRepository _weatherForecastRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<WeatherForecastService> _logger;
        private readonly IValidator<RequestCreateWeatherForecastDto> _createValidator;
        private readonly IValidator<RequestUpdateWeatherForecastDto> _updateValidator;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="weatherForecastRepository">WeatherForecastRepository's interface</param>
        /// <param name="mapper">Mapper's interface</param>
        /// <param name="logger">Logger's interface</param>
        /// <param name="createValidator">Validator of RequestCreateWeatherForecastDto</param>
        /// <param name="updateValidator">Validator of RequestUpdateWeatherForecastDto</param>
        /// <exception cref="ArgumentNullException"></exception>
        public WeatherForecastService(
            IWeatherForecastRepository weatherForecastRepository,
            IMapper mapper,
            ILogger<WeatherForecastService> logger,
            IValidator<RequestCreateWeatherForecastDto> createValidator,
            IValidator<RequestUpdateWeatherForecastDto> updateValidator)
        {
            _weatherForecastRepository = weatherForecastRepository ?? throw new ArgumentNullException(nameof(weatherForecastRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _updateValidator = updateValidator;
            _createValidator = createValidator;
        }

        #endregion

        #region IWeatherForecastService implementation

        ///<inheritdoc/>
        public async Task<ResponseGetWeatherForecastDto> CreateAsync(RequestCreateWeatherForecastDto request)
        {
            _logger.LogInformation($"{nameof(WeatherForecastService)} - Started method {nameof(CreateAsync)}");
            ValidatorExtension<RequestCreateWeatherForecastDto>.Validate(_createValidator, request);

            var weatherForecast = _mapper.Map<WeatherForecast>(request);
            var result = await _weatherForecastRepository.AddAsync(weatherForecast);
            await _weatherForecastRepository.SaveChangesAsync();

            return _mapper.Map<ResponseGetWeatherForecastDto>(result);
        }

        ///<inheritdoc/>
        public async Task<ResponseGetWeatherForecastDto> GetAsync(int id)
        {
            _logger.LogInformation($"{nameof(WeatherForecastService)} - Started method {nameof(GetAsync)}");
            var result = await _weatherForecastRepository.GetAsync(id);

            result.ValidateIfFound(id);

            return _mapper.Map<ResponseGetWeatherForecastDto>(result);
        }

        ///<inheritdoc/>
        public async Task<DtoBasePagination<ResponseGetWeatherForecastDto>> GetListAsync(RequestGetAllWeatherForecastDto request)
        {
            _logger.LogInformation($"{nameof(WeatherForecastService)} - Started method {nameof(GetListAsync)}");
            var result = await _weatherForecastRepository.GetListAsync(request.PageSize, request.CurrentPage, request.IsPaged, request.OrderBy, request.isDescending);

            return await GetDtoBasePagination(request, result);
        }

        ///<inheritdoc/>
        public async Task RemoveAsync(int id)
        {
            _logger.LogInformation($"{nameof(WeatherForecastService)} - Started method {nameof(RemoveAsync)}");
            var weatherForecast = await _weatherForecastRepository.GetAsync(id);

            weatherForecast.ValidateIfFound(id);

            _weatherForecastRepository.Remove(weatherForecast);
            await _weatherForecastRepository.SaveChangesAsync();
        }

        ///<inheritdoc/>
        public async Task UpdateAsync(RequestUpdateWeatherForecastDto request)
        {
            _logger.LogInformation($"{nameof(WeatherForecastService)} - Started method {nameof(UpdateAsync)}");
            ValidatorExtension<RequestUpdateWeatherForecastDto>.Validate(_updateValidator, request);

            var requestFinded = await _weatherForecastRepository.GetAsync(request.Id);
            requestFinded.ValidateIfFound(request.Id);

            WeatherForecast weatherForecast = _mapper.Map(request, requestFinded);
            _weatherForecastRepository.Update(weatherForecast);
            await _weatherForecastRepository.SaveChangesAsync();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Obtains a request paginated
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="result">Response to the query</param>
        /// <returns>An object containing the paged response</returns>
        private async Task<DtoBasePagination<ResponseGetWeatherForecastDto>> GetDtoBasePagination(RequestGetAllWeatherForecastDto request, IEnumerable<WeatherForecast> result)
        {
            int resultCount = result.Count() == 0 ? 1 : result.Count();

            DtoBasePagination<ResponseGetWeatherForecastDto> response = new()
            {
                CurrentPage = request.CurrentPage ?? 1,
                PageSize = request.PageSize ?? resultCount,
                Items = _mapper.Map<List<ResponseGetWeatherForecastDto>>(result)
            };
            response.PageCount = resultCount / response.PageSize;
            response.RowCount = await _weatherForecastRepository.GetTotalCount();

            return response;
        }

        #endregion
    }
}

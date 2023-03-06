using Cross_Cutting.Extensions;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// WeatherForecast class repository
    /// </summary>
    internal class WeatherForecastRepository : GenericRepository<WeatherForecast, DummyContext>, IWeatherForecastRepository
    {
        #region Properties

        private readonly IDbConnection connection;
        private readonly ILogger<WeatherForecastRepository> _logger;

        private const string QUERY_GET_ALL = @"select [ID], [DATE], [TEMPERATURE_C] as TEMPERATUREC, [SUMMARY] from [DummyDb].[dbo].[WEATHERFORECAST]";
        private const string QUERY_COUNT_ALL = @"select count(ID) from [DummyDb].[dbo].[WEATHERFORECAST]";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration interface</param>
        /// <param name="context">Context</param>
        /// <param name="logger">Logger</param>
        /// <exception cref="ArgumentNullException"></exception>
        public WeatherForecastRepository(IConfiguration configuration, DummyContext context, ILogger<WeatherForecastRepository> logger) : base(context, logger)
        {
            connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region IWeatherForecastRepository's implementation

        ///<inheritdoc/>
        public async Task<WeatherForecast> GetAsync(int id)
        {
            _logger.LogInformation($"{nameof(WeatherForecastRepository)} - Started method {nameof(GetAsync)}");
            var param = new
            {
                Id = id
            };

            var sql = QUERY_GET_ALL + "where Id = @Id;";

            return await connection.QuerySingleOrDefaultAsync<WeatherForecast>(sql, param);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<WeatherForecast>> GetListAsync(int? pageSize, int? currentPage, bool isPaged, string orderBy, bool isDescending)
        {
            _logger.LogInformation($"{nameof(WeatherForecastRepository)} - Started method {nameof(GetListAsync)}");

            var sql = QUERY_GET_ALL;
            object? param = null;

            DapperExtension.GetOrderBy(orderBy, isDescending, ref sql);
            DapperExtension.GetPagination(pageSize, currentPage, isPaged, ref sql, ref param);

            return await connection.QueryAsync<WeatherForecast>(sql, param);
        }

        ///<inheritdoc/>
        public async Task<int> GetTotalCount()
        {
            _logger.LogInformation($"{nameof(WeatherForecastRepository)} - Started method {nameof(GetTotalCount)}");

            return await connection.QuerySingleAsync<int>(QUERY_COUNT_ALL);
        }

        #endregion
    }
}

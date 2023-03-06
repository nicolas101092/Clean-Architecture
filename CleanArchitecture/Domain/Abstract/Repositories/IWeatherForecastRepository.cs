namespace Domain.Abstract.Repositories
{
    /// <summary>
    /// WeatherForecast interface's repository
    /// </summary>
    public interface IWeatherForecastRepository
    {
        /// <summary>
        /// Insert a new WeatherForecast into database
        /// </summary>
        /// <param name="entity">WeatherForecast</param>
        /// <returns>Returns the WeatherForecast inserted</returns>
        Task<WeatherForecast> AddAsync(WeatherForecast entity);

        /// <summary>
        /// Removes a requested WeatherForecast from the database
        /// </summary>
        /// <param name="entity">WeatherForecast</param>
        void Remove(WeatherForecast entity);

        /// <summary>
        /// Save all changes into database
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Updates a requested WeatherForecast into the database
        /// </summary>
        /// <param name="entity">WeatherForecast</param>
        void Update(WeatherForecast entity);

        /// <summary>
        /// Gets a WeatherForecast filtered by Id
        /// </summary>
        /// <param name="id">Identifier of WeatherForecast</param>
        /// <returns>Returns the requested WeatherForecast</returns>
        Task<WeatherForecast> GetAsync(int id);

        /// <summary>
        /// Gets a WeatherForecast list paginated and ordered
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="isPaged">Indicate if we want pagination</param>
        /// <param name="orderBy">Name of the column to be sorted</param>
        /// <param name="isDescending">Defines if sort is ascending or descending</param>
        /// <returns>Returns a list of WeatherForecast</returns>
        Task<IEnumerable<WeatherForecast>> GetListAsync(int? pageSize, int? currentPage, bool isPaged, string orderBy, bool isDescending);

        /// <summary>
        /// Gets a WeatherForecast not tracked filtered by Id
        /// </summary>
        /// <param name="id">Identifier of WeatherForecast</param>
        /// <returns>Returns the requested WeatherForecas</returns>
        Task<WeatherForecast?> FindById(int id);

        /// <summary>
        /// Gets the total number of elements in the WeatherForecast table
        /// </summary>
        /// <returns></returns>
        Task<int> GetTotalCount();
    }
}

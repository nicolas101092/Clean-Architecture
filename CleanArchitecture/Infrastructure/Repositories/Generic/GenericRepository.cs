namespace Infrastructure.Repositories.Generic
{
    /// <summary>
    /// Generic class repository
    /// </summary>
    /// <typeparam name="T">Generic class type</typeparam>
    /// <typeparam name="TContext">Generic context type</typeparam>
    public class GenericRepository<T, TContext> : IGenericRepository<T, TContext>
        where T : class
        where TContext : DbContext
    {
        #region Properties

        protected readonly TContext _context;
        private readonly ILogger<GenericRepository<T, TContext>> _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Generic context type</param>
        /// <param name="logger">Logger</param>
        /// <exception cref="ArgumentNullException"></exception>
        public GenericRepository(TContext context, ILogger<GenericRepository<T, TContext>> logger)
        {
            _context = context;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Implementation IGenericRepository

        ///<inheritdoc/>
        public async Task<T> AddAsync(T entity)
        {
            _logger.LogInformation($"{typeof(T).Name}Repository - Started method {nameof(AddAsync)}");
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        ///<inheritdoc/>
        public void Update(T entity)
        {
            _logger.LogInformation($"{typeof(T).Name}Repository - Started method {nameof(Update)}");
            _context.Set<T>().Update(entity);
        }

        ///<inheritdoc/>
        public void Remove(T entity)
        {
            _logger.LogInformation($"{typeof(T).Name}Repository - Started method {nameof(Remove)}");
            _context.Set<T>().Remove(entity);
        }

        ///<inheritdoc/>
        public async Task<T?> FindById(int id)
        {
            _logger.LogInformation($"{typeof(T).Name}Repository - Started method {nameof(FindById)}");
            return await _context.Set<T>().FindAsync(id);
        }

        ///<inheritdoc/>
        public async Task SaveChangesAsync()
        {
            _logger.LogInformation($"{typeof(T).Name}Repository - Started method {nameof(SaveChangesAsync)}");
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}

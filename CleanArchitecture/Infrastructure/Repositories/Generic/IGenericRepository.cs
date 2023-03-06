namespace Infrastructure.Repositories.Generic
{
    /// <summary>
    /// Generic interface's repository
    /// </summary>
    /// <typeparam name="T">Generic class type</typeparam>
    /// <typeparam name="TContext">Generic context type</typeparam>
    public interface IGenericRepository<T, TContext>
        where T : class
        where TContext : class
    {
        /// <summary>
        /// Insert a new generic class into database
        /// </summary>
        /// <param name="entity">generic class</param>
        /// <returns>Returns the generic class inserted</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Gets a generic class not tracked filtered by Id
        /// </summary>
        /// <param name="id">Identifier of generic class</param>
        /// <returns>Returns the requested generic class</returns>
        Task<T?> FindById(int id);

        /// <summary>
        /// Removes a requested generic class from the database
        /// </summary>
        /// <param name="entity">generic class</param>
        void Remove(T entity);

        /// <summary>
        /// Save all changes into database
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Updates a requested generic class into the database
        /// </summary>
        /// <param name="entity">generic class</param>
        void Update(T entity);
    }
}

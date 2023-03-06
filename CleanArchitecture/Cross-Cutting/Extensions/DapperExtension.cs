namespace Cross_Cutting.Extensions
{
    /// <summary>
    /// Static class defining extension methods for Dapper
    /// </summary>
    public static class DapperExtension
    {
        #region Constants

        private const string QUERY_PAGINATION = @" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        #endregion

        #region Methods

        /// <summary>
        /// Define the sql statement by adding the sort order 
        /// </summary>
        /// <param name="orderBy">Name of the column to be sorted</param>
        /// <param name="isDescending">Defines if sort is ascending or descending</param>
        /// <param name="sql">Sentence sql</param>
        public static void GetOrderBy(string orderBy, bool isDescending, ref string sql)
        {
            var orderCriteria = isDescending ? " DESC" : " ASC";
            sql += @$" ORDER BY '{orderBy}'" + orderCriteria;
        }

        /// <summary>
        /// Define the sql statement by adding the pagination
        /// </summary>
        /// <param name="pageSize">Page size</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="isPaged">Indicate if we want pagination</param>
        /// <param name="sql">Sentence sql</param>
        /// <param name="param">Params of sentence</param>
        public static void GetPagination(int? pageSize, int? currentPage, bool isPaged, ref string sql, ref object? param)
        {
            if (isPaged)
            {
                param = new
                {
                    Offset = (currentPage - 1) * pageSize,
                    PageSize = pageSize
                };

                sql += QUERY_PAGINATION;
            }
        }

        #endregion
    }
}

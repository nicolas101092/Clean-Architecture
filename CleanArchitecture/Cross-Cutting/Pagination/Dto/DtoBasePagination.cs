namespace Cross_Cutting.Pagination.Dto
{
    /// <summary>
    /// DtoBasePagination's class.
    /// </summary>
    public class DtoBasePagination<TDto>
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public List<TDto> Items { get; set; } = null!;
    }
}

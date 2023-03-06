using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cross_Cutting.Pagination.Dto
{
    /// <summary>
    /// DtoListFiltersBase's class.
    /// </summary>
    public class DtoListFiltersBase
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }

        [BindNever]
        public bool IsPaged { get => CurrentPage != null && PageSize != null; }
    }
}

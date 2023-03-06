using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cross_Cutting.Middlewares.Dto
{
    /// <summary>
    /// Deto adding errors to DtoDetailException
    /// </summary>
    public class DtoDetailValidationException : DtoDetailException
    {
        public ModelStateDictionary Errors { get; set; } = null!;
    }
}

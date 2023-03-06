using FluentValidation;

namespace Cross_Cutting.Extensions
{
    /// <summary>
    /// Extension for make validation in models
    /// </summary>
    /// <typeparam name="T">Generic class</typeparam>
    public static class ValidatorExtension<T>
        where T : class
    {
        /// <summary>
        /// Method for fluent validation
        /// </summary>
        /// <param name="validator">Generic validator</param>
        /// <param name="dto">Object to evaluate</param>
        public static void Validate(IValidator<T> validator, T dto)
        {
            var validationResult = validator.Validate(dto);
            ModelValidationExtension.ValidateBoolLogic(validationResult.IsValid, validationResult?.Errors?.FirstOrDefault()?.ErrorMessage);
        }
    }
}

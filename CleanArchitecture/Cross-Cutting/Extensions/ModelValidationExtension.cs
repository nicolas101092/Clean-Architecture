using Cross_Cutting.Extensions.ExceptionExtensions;

namespace Cross_Cutting.Extensions
{
    /// <summary>
    /// Static class defining the methods that catch exceptions
    /// </summary>
    public static class ModelValidationExtension
    {
        #region Constants

        public const string GENERIC_MESSAGE_VALIDATION = "One or more validation errors found.";

        #endregion

        #region Methods

        /// <summary>
        /// Validates if an object is null and throw personal exception.
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam 
        /// <param name="item">Generic class</param>
        public static void ValidateIfNull<T>(this T item)
        {
            if (item == null)
            {
                throw new BadRequestException();
            }
        }

        /// <summary>
        /// Check if Model found is null and throw personal exception.
        /// </summary>
        /// <typeparam name="T">Generit type</typeparam>
        /// <param name="item">Generic object type</param>
        /// <param name="id">Int object type</param>
        public static void ValidateIfFound<T>(this T item, int id)
        {
            if (item == null)
            {
                throw new NotFoundException(id.ToString());
            }
        }

        /// <summary>
        /// Check if Model found is null and throw personal exception.
        /// </summary>
        /// <typeparam name="T">Generit type</typeparam>
        /// <param name="item">Generic object type</param>
        /// <param name="id">string with id of object type</param>
        public static void ValidateIfFound<T>(this T item, string id)
        {
            if (item == null)
            {
                throw new NotFoundException(id);
            }
        }

        /// <summary>
        /// Check if parse logic of Dto and throw personal exception.
        /// </summary>
        /// <param name="valid">Bool object type</param>
        /// <param name="message">String object type</param>
        public static void ValidateBoolLogic(bool valid, string message = GENERIC_MESSAGE_VALIDATION)
        {
            if (!valid)
            {
                throw new BadRequestException(message);
            }
        }

        #endregion
    }
}



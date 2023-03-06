using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cross_Cutting.Extensions.ExceptionExtensions
{
    /// <summary>
    /// BadRequestException's Class.
    /// </summary>
    [Serializable]
    public class BadRequestException : Exception
    {
        public ModelStateDictionary Errors { get; set; }

        public BadRequestException() : base(ConstantsExceptions.MESSAGE_ITEM_NULL_EXCEPTION)
        {
        }

        public BadRequestException(string id) : base(id)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BadRequestException(ModelStateDictionary errors, string message = ConstantsExceptions.MESSAGE_SEVERAL_ERRORS) : base(message)
        {
            Errors = errors;
        }

        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

namespace Cross_Cutting.Extensions.ExceptionExtensions
{
    /// <summary>
    /// ConflictedException's Class.
    /// </summary>
    [Serializable]
    public class ConflictedException : Exception
    {
        public ConflictedException() : base(ConstantsExceptions.MESSAGE_CONFLICTED_EXCEPTION)
        {
        }

        public ConflictedException(string message) : base(message)
        {
        }

        public ConflictedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConflictedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

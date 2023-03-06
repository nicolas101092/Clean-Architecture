namespace Cross_Cutting.Extensions.ExceptionExtensions
{
    /// <summary>
    /// NotFoundException's Class.
    /// </summary>
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException() : base(ConstantsExceptions.MESSAGE_NOT_FOUND_EXCEPTION)
        {
        }

        public NotFoundException(string id) : base($"{ConstantsExceptions.MESSAGE_NOT_FOUND_EXCEPTION}: {id}")
        {
        }
    }
}

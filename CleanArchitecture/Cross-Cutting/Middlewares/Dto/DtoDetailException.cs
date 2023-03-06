namespace Cross_Cutting.Middlewares.Dto
{
    /// <summary>
    /// DtoBaseException Class.
    /// </summary>
    public class DtoDetailException
    {
        public string Title { get; set; } = null!;
        public int Status { get; set; }
        public string Trace { get; set; } = null!;
        public string Detail { get; set; } = null!;

    }
}

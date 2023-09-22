namespace GBReaderJacqueminS.Repositories
{
    public class PageException : Exception
    {
        public PageException(string message, Exception ex)
            : base(message, ex)
        { }
    }
}

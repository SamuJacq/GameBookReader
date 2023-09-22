namespace GBReaderJacqueminS.Repositories
{
    public class BookException : Exception
    {
        public BookException(string message, Exception ex)
            : base(message, ex) { }
    }
}

namespace GBReaderJacqueminS.Repositories
{
    public class UnableToConnectException : Exception
    {
        public UnableToConnectException(string message, Exception ex)
            : base(message, ex)
        { }
    }
}

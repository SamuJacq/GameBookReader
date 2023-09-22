namespace GBReaderJacqueminS.Repositories
{
    public class SaveException : Exception
    {
        public SaveException(string message, Exception ex)
            : base(message, ex) { }
    }
}

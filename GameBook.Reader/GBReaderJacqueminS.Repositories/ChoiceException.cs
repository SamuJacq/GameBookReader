namespace GBReaderJacqueminS.Repositories
{
    public class ChoiceException : Exception
    {
        public ChoiceException(string message, Exception ex)
            : base(message, ex)
        { }
    }
}

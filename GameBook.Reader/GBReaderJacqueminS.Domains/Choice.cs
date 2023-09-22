namespace GBReaderJacqueminS.Domains
{
    public record Choice
    {

        private string _summary;
        private int _numPage;

        public Choice(string summary, int numPage)
        {
            _summary = summary;
            _numPage = numPage;
        }

        public string Summary => _summary;

        public int NumPage => _numPage;

    }
}

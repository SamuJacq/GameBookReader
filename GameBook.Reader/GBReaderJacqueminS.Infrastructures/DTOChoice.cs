namespace GBReaderJacqueminS.Infrastructures
{
    public record DTOChoice
    {
        private string _summary;
        private int _numPage;

        public DTOChoice(string summary, int numPage) { 
            _summary = summary;
            _numPage = numPage;
        }

        public string Summary { 
            get => _summary;
            set => _summary = value;
        }

        public int NumPage { 
            get => _numPage;
            set => _numPage = value;
        }
    }
}

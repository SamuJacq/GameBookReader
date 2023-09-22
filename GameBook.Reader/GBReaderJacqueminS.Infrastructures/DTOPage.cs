namespace GBReaderJacqueminS.Infrastructures
{
    public record DTOPage
    {

        private int _numPage;
        private string _contain;
        private List<DTOChoice> _choices;

        public DTOPage(int NumPage, string contain) { 
            _numPage = NumPage;
            _contain = contain;
            _choices = new List<DTOChoice>();
        }

        public int NumPage { 
            get => _numPage; 
            set => _numPage = value;
        }

        public string Contain
        {
            get => _contain;
            set => _contain = value;
        }

        public List<DTOChoice> Choices {
            get => _choices;
            set => _choices = value;
        }

    }
}

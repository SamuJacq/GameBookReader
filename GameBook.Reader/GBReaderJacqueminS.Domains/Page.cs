namespace GBReaderJacqueminS.Domains
{
    public record Page
    {

        private string _contain;
        private List<Choice> _listChoice;

        public Page(string contain)
        {
            _contain = string.IsNullOrEmpty(contain) ? "contain unknown" : contain;
            _listChoice = new List<Choice>();
        }

        public string Contain => _contain;

        public List<Choice> ListChoice
        {
            get => _listChoice;
            set => _listChoice = value == null ? new List<Choice>() : value;
        }

    }
}

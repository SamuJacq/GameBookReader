namespace GBReaderJacqueminS.Infrastructures
{
    public record DTOBook
    {

        private string _title;
        private string _isbn;
        private string _resume;
        private string _author;

        public DTOBook(string title, string isbn, string resume, string author) { 
            _title = title;
            _isbn = isbn;
            _resume = resume;
            _author = author;
        }

        public string Title {
            get => _title;
            set => _title = value;
        }

        public string Isbn
        {
            get => _isbn;
            set => _isbn = value;
        }

        public string Resume
        {
            get => _resume;
            set => _resume = value;
        }

        public string Author
        {
            get => _author;
            set => _author = value;
        }

    }
}

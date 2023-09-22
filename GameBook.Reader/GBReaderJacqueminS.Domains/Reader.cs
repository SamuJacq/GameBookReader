namespace GBReaderJacqueminS.Domains
{
    public class Reader
    {
        private Book _currentBook;
        private Stat _stat;
        private int _numPage;

        public Reader() { 
            _currentBook = new Book("", "", "", "");
            _stat = new Stat(DateTime.Now.ToString(), DateTime.Now.ToString());   
        }

        public Book CurrentBook
        {
            get => _currentBook;
            set => _currentBook = value;
        }

        public Stat Stat
        {
            get => _stat;
            set => _stat = value;
        }

        public int NumPage
        {
            get => _numPage == 0 ? 1 : _numPage;
            set => _numPage = value;
        }
    }
}

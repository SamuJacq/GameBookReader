namespace GBReaderJacqueminS.Domains
{
    public class Book
    {
        private string _title;
        private string _isbn;
        private string _resume;
        private string _author;
        private List<Page> _pages;

        public Book(string title, string isbn, string resume, string author)
        {
            _title = string.IsNullOrEmpty(title) ? "unknown title" : title;
            _isbn = string.IsNullOrEmpty(isbn) ? "unknown isbn" : isbn;
            _resume = string.IsNullOrEmpty(resume) ? "no resume" : resume;
            _author = string.IsNullOrEmpty(resume) ? "unknown author" : author;
            _pages = new List<Page>();
        }

        public string Title => _title;
        public string Isbn => _isbn;
        public string Resume => _resume;
        public string Author => _author;
        public List<Page> Pages => _pages;

        public void AddPage(List<Page> list)
        {
            if (list == null)
            {
                return;
            }
            _pages = list;
        }

        public Page GetPage(int index)
        {
            if (index < 0 || index >= _pages.Count)
            {
                return new Page("unknown contain");
            }
            return _pages[index];
        }

    }
}
